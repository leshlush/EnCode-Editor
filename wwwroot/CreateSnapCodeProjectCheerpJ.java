import java.io.*;
import java.util.Base64;

public class CreateSnapCodeProjectCheerpJ {
    public static void main(String[] args) throws Exception {
        File jsonFile = new File("/str/project.json");
        if (!jsonFile.exists()) {
            System.err.println("project.json not found in /str/");
            return;
        }

        String json = readAll(jsonFile);
        if (json.startsWith("\"") && json.endsWith("\"")) {
            // Remove outer quotes
            json = json.substring(1, json.length() - 1);
            // Unescape inner quotes and backslashes
            json = json.replaceAll("\\\\\"", "\"").replaceAll("\\\\\\\\", "\\\\");
        }

        // Find the Files array
        int filesStart = json.indexOf("\"Files\"");
        if (filesStart < 0) {
            System.err.println("No 'Files' array found in project.json");
            return;
        }
        int arrayStart = json.indexOf('[', filesStart);
        int arrayEnd = findMatchingBracket(json, arrayStart);
        if (arrayStart < 0 || arrayEnd < 0) {
            System.err.println("Malformed 'Files' array in project.json");
            return;
        }
        String filesArray = json.substring(arrayStart, arrayEnd + 1);

        processFilesArray(filesArray, "/files");
    }

    // Recursively process a JSON array of files/folders
    private static void processFilesArray(String filesArray, String parentPath) {
        int idx = 0;
        while (idx < filesArray.length()) {
            int objStart = filesArray.indexOf('{', idx);
            if (objStart < 0) break;
            int objEnd = findMatchingBrace(filesArray, objStart);
            if (objEnd < 0) break;
            String entry = filesArray.substring(objStart, objEnd + 1);

            String path = extractJsonValue(entry, "path");
            String content = extractJsonValue(entry, "content");
            String isBinaryStr = extractJsonValue(entry, "isBinary");
            String isDirectoryStr = extractJsonValue(entry, "isDirectory");
            boolean isBinary = isBinaryStr != null && isBinaryStr.equalsIgnoreCase("true");
            boolean isDirectory = isDirectoryStr != null && isDirectoryStr.equalsIgnoreCase("true");

            if (path == null || path.isEmpty()) {
                System.err.println("Skipping file with missing path.");
                idx = objEnd + 1;
                continue;
            }
            String fullPath = parentPath + (path.startsWith("/") ? path : "/" + path);

            if (isDirectory) {
                File dir = new File(fullPath);
                if (!dir.exists()) dir.mkdirs();
                // Recursively process children
                String childrenArray = extractJsonArray(entry, "children");
                if (childrenArray != null && childrenArray.trim().length() > 2) { // not empty array
                    processFilesArray(childrenArray, fullPath);
                }
            } else {
                if (content == null) {
                    System.err.println("Warning: File '" + fullPath + "' has null content. Writing as empty file.");
                    content = "";
                }
                File outFile = new File(fullPath);
                File parentDir = outFile.getParentFile();
                if (parentDir != null && !parentDir.exists()) parentDir.mkdirs();

                try {
                    if (isBinary) {
                        try (OutputStream out = new FileOutputStream(outFile)) {
                            byte[] bytes;
                            try {
                                bytes = Base64.getDecoder().decode(content);
                            } catch (IllegalArgumentException e) {
                                System.err.println("Warning: File '" + fullPath + "' has invalid base64 content. Writing empty file.");
                                bytes = new byte[0];
                            }
                            out.write(bytes);
                        }
                     
                    }
                    else {
                        // Unescape content before writing for text files
                        String fixedContent = JsonUnescape.unescapeJsonString(content);
                         System.out.println("WRITING FILE: " + outFile.getAbsolutePath());
                            System.out.println("--- FILE CONTENT START ---");
                            System.out.println(fixedContent);
                            System.out.println("--- FILE CONTENT END ---");
                        try (Writer out = new OutputStreamWriter(new FileOutputStream(outFile), "UTF-8")) {
                            out.write(fixedContent);
                        }
                    }
                    System.out.println("Wrote file: " + outFile.getAbsolutePath());
                } catch (Exception ex) {
                    System.err.println("Error writing file '" + fullPath + "': " + ex.getMessage());
                    ex.printStackTrace();
                }
            }
            idx = objEnd + 1;
        }
    }

    private static String readAll(File file) throws IOException {
        try (BufferedReader r = new BufferedReader(new FileReader(file))) {
            StringBuilder sb = new StringBuilder();
            String line;
            while ((line = r.readLine()) != null) sb.append(line);
            return sb.toString();
        }
    }

    // Extracts a string value for a given key from a JSON object string
    private static String extractJsonValue(String json, String key) {
        int idx = json.indexOf("\"" + key + "\"");
        if (idx < 0) return null;
        int colon = json.indexOf(':', idx);
        if (colon < 0) return null;
        int valueStart = colon + 1;
        // Skip whitespace
        while (valueStart < json.length() && Character.isWhitespace(json.charAt(valueStart))) valueStart++;
        if (valueStart >= json.length()) return null;
        if (json.charAt(valueStart) == '"') {
            int start = valueStart + 1;
            int end = valueStart + 1;
            while (end < json.length()) {
                if (json.charAt(end) == '"' && json.charAt(end - 1) != '\\') break;
                end++;
            }
            if (end >= json.length()) return null;
            return json.substring(start, end);
        } else if (json.charAt(valueStart) == 't' || json.charAt(valueStart) == 'f') {
            // true/false
            int end = valueStart;
            while (end < json.length() && Character.isLetter(json.charAt(end))) end++;
            return json.substring(valueStart, end);
        } else if (json.charAt(valueStart) == '[') {
            // Array, not handled here
            return null;
        } else {
            // null or number
            int end = valueStart;
            while (end < json.length() && !Character.isWhitespace(json.charAt(end)) && json.charAt(end) != ',' && json.charAt(end) != '}') end++;
            return json.substring(valueStart, end);
        }
    }

    // Extracts a JSON array string for a given key from a JSON object string
    private static String extractJsonArray(String json, String key) {
        int idx = json.indexOf("\"" + key + "\"");
        if (idx < 0) return null;
        int colon = json.indexOf(':', idx);
        if (colon < 0) return null;
        int arrayStart = json.indexOf('[', colon);
        if (arrayStart < 0) return null;
        int arrayEnd = findMatchingBracket(json, arrayStart);
        if (arrayEnd < 0) return null;
        return json.substring(arrayStart, arrayEnd + 1);
    }

    // Finds the matching closing bracket for a JSON array
    private static int findMatchingBracket(String json, int openIdx) {
        int depth = 0;
        for (int i = openIdx; i < json.length(); i++) {
            if (json.charAt(i) == '[') depth++;
            else if (json.charAt(i) == ']') {
                depth--;
                if (depth == 0) return i;
            }
        }
        return -1;
    }

    // Finds the matching closing brace for a JSON object
    private static int findMatchingBrace(String json, int openIdx) {
        int depth = 0;
        for (int i = openIdx; i < json.length(); i++) {
            if (json.charAt(i) == '{') depth++;
            else if (json.charAt(i) == '}') {
                depth--;
                if (depth == 0) return i;
            }
        }
        return -1;
    }
}

// Place JsonUnescape as a separate class (can be in the same file)
class JsonUnescape {
    /**
     * Manually unescape a JSON string literal to plain text.
     * Handles \n, \t, \r, \\, \", and basic \\uXXXX unicode escapes.
     */
    public static String unescapeJsonString(String s) {
        StringBuilder sb = new StringBuilder();
        int len = s.length();
        for (int i = 0; i < len; ) {
            char c = s.charAt(i);
            if (c == '\\' && i + 1 < len) {
                char next = s.charAt(i + 1);
                switch (next) {
                    case 'n': sb.append('\n'); i += 2; break;
                    case 't': sb.append('\t'); i += 2; break;
                    case 'r': sb.append('\r'); i += 2; break;
                    case 'b': sb.append('\b'); i += 2; break;
                    case 'f': sb.append('\f'); i += 2; break;
                    case '\\': sb.append('\\'); i += 2; break;
                    case '\"': sb.append('\"'); i += 2; break;
                    case 'u':
                        if (i + 6 <= len) {
                            String hex = s.substring(i + 2, i + 6);
                            try {
                                int code = Integer.parseInt(hex, 16);
                                sb.append((char) code);
                                i += 6;
                            } catch (NumberFormatException e) {
                                sb.append("\\u").append(hex);
                                i += 6;
                            }
                        } else {
                            sb.append("\\u");
                            i += 2;
                        }
                        break;
                    default: // unknown escape, copy literally
                        sb.append(c).append(next);
                        i += 2;
                        break;
                }
            } else {
                sb.append(c);
                i++;
            }
        }
        return sb.toString();
    }
}