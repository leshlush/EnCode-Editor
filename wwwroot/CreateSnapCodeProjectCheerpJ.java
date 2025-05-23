import java.io.*;

public class CreateSnapCodeProjectCheerpJ {
    public static void main(String[] args) throws Exception {
        File strDir = new File("/str");
        copyAll(strDir, new File("/files"), strDir.getAbsolutePath());
    }

   private static void copyAll(File src, File destRoot, String strRootPath) throws IOException {
        if (src.isDirectory()) {
            for (File child : src.listFiles()) {
                copyAll(child, destRoot, strRootPath);
            }
        } else if (src.isFile()) {
            String srcPath = src.getAbsolutePath();
            String relativePath = srcPath.substring(strRootPath.length());
            // If the file is at the root, relativePath will be like "/hello.txt"
            // Remove leading slash if present
            if (relativePath.startsWith(File.separator)) {
                relativePath = relativePath.substring(1);
            }
            // Only proceed if relativePath is not empty
            if (!relativePath.isEmpty()) {
                File destFile = new File(destRoot, relativePath);
                destFile.getParentFile().mkdirs();

                try (FileInputStream in = new FileInputStream(src);
                     FileOutputStream out = new FileOutputStream(destFile)) {
                    byte[] buf = new byte[4096];
                    int len;
                    while ((len = in.read(buf)) > 0) {
                        out.write(buf, 0, len);
                    }
                }
                System.out.println("Copied " + srcPath + " to " + destFile.getAbsolutePath());
            }
        }
    }
}
