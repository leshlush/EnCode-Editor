"use strict";(self.wpRiseJsonp=self.wpRiseJsonp||[]).push([["vendors-node_modules_pnpm_articulate_design-system_1_5_0__articulate_design-system-tokens_1_1-d24749"],{24680:(vo,Gr,ur)=>{ur.r(Gr),ur.d(Gr,{Arc:()=>$n,ArcAvatar:()=>Dr,ArcAvatarColors:()=>za,ArcAvatarColorsReserved:()=>ze,ArcAvatarOverflow:()=>ct,ArcAvatarOverflowSizes:()=>Ee,ArcAvatarPile:()=>He,ArcAvatarPileSizes:()=>_e,ArcAvatarSizes:()=>Ce,ArcAvatarText:()=>Oe,ArcAvatarTextSizes:()=>Se,ArcBadge:()=>gt,ArcBadgeContrasts:()=>bt,ArcBadgeCount:()=>kt,ArcBadgeCountShapes:()=>wt,ArcBadgeCountSizes:()=>xt,ArcBadgeCountVariants:()=>yt,ArcBadgeLayouts:()=>ut,ArcBadgeSizes:()=>ht,ArcBadgeVariants:()=>mt,ArcButton:()=>W,ArcButtonLayouts:()=>Je,ArcButtonShapes:()=>Ke,ArcButtonSizes:()=>Ge,ArcButtonVariants:()=>Qe,ArcCheckbox:()=>Ma,ArcCheckboxBar:()=>Mt,ArcCheckboxBarLayouts:()=>Rt,ArcCheckboxBarOption:()=>Pt,ArcCheckboxBarSizes:()=>Lt,ArcCheckboxLayouts:()=>jt,ArcCheckboxSizes:()=>$t,ArcCheckboxVariants:()=>Tt,ArcIcon:()=>w,ArcIconText:()=>pr,ArcIconTextLayouts:()=>dt,ArcIconTextSizes:()=>st,ArcIconTextVariants:()=>pt,ArcInputAction:()=>Ot,ArcLabel:()=>Q,ArcLabelContent:()=>rr,ArcLabelLayouts:()=>zt,ArcLink:()=>Ut,ArcLinkLayouts:()=>Ht,ArcLinkSizes:()=>Vt,ArcLinkVariants:()=>Nt,ArcPopover:()=>qr,ArcPopoverAnchors:()=>Ha,ArcPopoverBoundaries:()=>Va,ArcPopoverDefaults:()=>M,ArcPopoverModes:()=>Oa,ArcPopoverVariants:()=>Ba,ArcPopoverVariantsWithOutline:()=>Ft,ArcRadio:()=>Ga,ArcRadioBar:()=>hn,ArcRadioBarLayouts:()=>dn,ArcRadioBarOption:()=>vn,ArcRadioBarSizes:()=>pn,ArcRadioLayouts:()=>cn,ArcRadioSizes:()=>ln,ArcSwitch:()=>mn,ArcSwitchSizes:()=>un,ArcSwitchStates:()=>E,ArcSystemAlignments:()=>Jr,ArcSystemColors:()=>Ir,ArcSystemLayouts:()=>P,ArcSystemLineHeights:()=>Qr,ArcSystemPopoverModes:()=>Tr,ArcSystemShapes:()=>$r,ArcSystemSizes:()=>_,ArcSystemVariants:()=>$,ArcTextAlignments:()=>zn,ArcTextBodyElements:()=>Qa,ArcTextBodySizes:()=>kn,ArcTextBodyVariants:()=>An,ArcTextElements:()=>re,ArcTextHeadingElements:()=>Ja,ArcTextHeadingSizes:()=>xn,ArcTextHeadingVariants:()=>wn,ArcTextInput:()=>ro,ArcTextInputLayouts:()=>Kn,ArcTextInputSizes:()=>Gn,ArcTextInputTypes:()=>Qn,ArcTextInputVariants:()=>Jn,ArcTextKinds:()=>gn,ArcTextLineHeights:()=>Cn,ArcTextSubheadingSizes:()=>yn,ArcTextTitleSizes:()=>fn,ArcToast:()=>ae,ArcToastPlacements:()=>oo,ArcToastPopoverModes:()=>no,ArcToastSizes:()=>eo,ArcToastVariants:()=>to,LogoArticulate:()=>Sa,LogoProductAI:()=>_a,LogoProductPeek:()=>Ea,LogoProductReach:()=>ja,LogoProductReview:()=>$a,LogoProductRise:()=>Ta,LogoProductStoryline:()=>Ia,adoptStyles:()=>x,assignRef:()=>ie,clamp:()=>ra,css:()=>m,didElementRectChange:()=>aa,extractTextFromReactNode:()=>dr,getPopoverId:()=>Dt,isDifferent:()=>D,isSame:()=>br,joinArray:()=>co,makeHtmlId:()=>N,mapDeprecated:()=>oe,pickFromList:()=>h,updatePopoverPosition:()=>Zr});const $r=["circle","square","narrow"],$=["inherit","common","disabled","neutral","outline","solid","ai","primary","secondary","tertiary","inverse","critical-primary","critical-secondary","ai-primary","ai-secondary","primary-inverse","secondary-inverse","tertiary-inverse","overlay-light","overlay-dark","brand-black","brand-blue","brand","info","success","warning","critical"],P=["hug","fill","icon","gutter","inline"],Tr=["","auto","manual"],Jr=["inherit","start","left","justify","center","between","around","evenly","stretch","end","right"],Qr=["inherit","tight","standard","loose"],_=["inherit","xxl","xl","lg","md","sm","xs","xxs"],Ir=["blue","orange","mint","red","yellow","green","maroon","purple","neutral","white","black"],oe=(a,r)=>r[a]||a,h=(a,r)=>a.filter(e=>r.includes(e)),ie=(a,r)=>{a&&(typeof a=="function"?a(r):a.current=r)},ra=(a,r=0,e=100)=>Math.min(Math.max(a,r),e),D=(a,r)=>a!==r,br=(a,r)=>a===r,aa=({a,b:r})=>D(a?.top,r?.top)||D(a?.left,r?.left)||D(a?.width,r?.width)||D(a?.height,r?.height);var o=ur(50635),g=ur(59288);/**
 * @license
 * Copyright 2019 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */const mr=globalThis,Rr=mr.ShadowRoot&&(mr.ShadyCSS===void 0||mr.ShadyCSS.nativeShadow)&&"adoptedStyleSheets"in Document.prototype&&"replace"in CSSStyleSheet.prototype,Lr=Symbol(),ea=new WeakMap;class ta{constructor(r,e,t){if(this._$cssResult$=!0,t!==Lr)throw Error("CSSResult is not constructable. Use `unsafeCSS` or `css` instead.");this.cssText=r,this.t=e}get styleSheet(){let r=this.o;const e=this.t;if(Rr&&r===void 0){const t=e!==void 0&&e.length===1;t&&(r=ea.get(e)),r===void 0&&((this.o=r=new CSSStyleSheet).replaceSync(this.cssText),t&&ea.set(e,r))}return r}toString(){return this.cssText}}const ce=a=>new ta(typeof a=="string"?a:a+"",void 0,Lr),m=(a,...r)=>{const e=a.length===1?a[0]:r.reduce((t,n,i)=>t+(c=>{if(c._$cssResult$===!0)return c.cssText;if(typeof c=="number")return c;throw Error("Value passed to 'css' function must be a 'css' function result: "+c+". Use 'unsafeCSS' to pass non-literal values, but take care to ensure page security.")})(n)+a[i+1],a[0]);return new ta(e,a,Lr)},le=(a,r)=>{if(Rr)a.adoptedStyleSheets=r.map(e=>e instanceof CSSStyleSheet?e:e.styleSheet);else for(const e of r){const t=document.createElement("style"),n=mr.litNonce;n!==void 0&&t.setAttribute("nonce",n),t.textContent=e.cssText,a.appendChild(t)}},na=Rr?a=>a:a=>a instanceof CSSStyleSheet?(r=>{let e="";for(const t of r.cssRules)e+=t.cssText;return ce(e)})(a):a;/**
 * @license
 * Copyright 2017 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */const{is:se,defineProperty:de,getOwnPropertyDescriptor:pe,getOwnPropertyNames:ve,getOwnPropertySymbols:he,getPrototypeOf:ue}=Object,gr=globalThis,oa=gr.trustedTypes,be=oa?oa.emptyScript:"",me=gr.reactiveElementPolyfillSupport,nr=(a,r)=>a,Pr={toAttribute(a,r){switch(r){case Boolean:a=a?be:null;break;case Object:case Array:a=a==null?a:JSON.stringify(a)}return a},fromAttribute(a,r){let e=a;switch(r){case Boolean:e=a!==null;break;case Number:e=a===null?null:Number(a);break;case Object:case Array:try{e=JSON.parse(a)}catch{e=null}}return e}},ia=(a,r)=>!se(a,r),ca={attribute:!0,type:String,converter:Pr,reflect:!1,useDefault:!1,hasChanged:ia};Symbol.metadata??=Symbol("metadata"),gr.litPropertyMetadata??=new WeakMap;class K extends HTMLElement{static addInitializer(r){this._$Ei(),(this.l??=[]).push(r)}static get observedAttributes(){return this.finalize(),this._$Eh&&[...this._$Eh.keys()]}static createProperty(r,e=ca){if(e.state&&(e.attribute=!1),this._$Ei(),this.prototype.hasOwnProperty(r)&&((e=Object.create(e)).wrapped=!0),this.elementProperties.set(r,e),!e.noAccessor){const t=Symbol(),n=this.getPropertyDescriptor(r,t,e);n!==void 0&&de(this.prototype,r,n)}}static getPropertyDescriptor(r,e,t){const{get:n,set:i}=pe(this.prototype,r)??{get(){return this[e]},set(c){this[e]=c}};return{get:n,set(c){const s=n?.call(this);i?.call(this,c),this.requestUpdate(r,s,t)},configurable:!0,enumerable:!0}}static getPropertyOptions(r){return this.elementProperties.get(r)??ca}static _$Ei(){if(this.hasOwnProperty(nr("elementProperties")))return;const r=ue(this);r.finalize(),r.l!==void 0&&(this.l=[...r.l]),this.elementProperties=new Map(r.elementProperties)}static finalize(){if(this.hasOwnProperty(nr("finalized")))return;if(this.finalized=!0,this._$Ei(),this.hasOwnProperty(nr("properties"))){const e=this.properties,t=[...ve(e),...he(e)];for(const n of t)this.createProperty(n,e[n])}const r=this[Symbol.metadata];if(r!==null){const e=litPropertyMetadata.get(r);if(e!==void 0)for(const[t,n]of e)this.elementProperties.set(t,n)}this._$Eh=new Map;for(const[e,t]of this.elementProperties){const n=this._$Eu(e,t);n!==void 0&&this._$Eh.set(n,e)}this.elementStyles=this.finalizeStyles(this.styles)}static finalizeStyles(r){const e=[];if(Array.isArray(r)){const t=new Set(r.flat(1/0).reverse());for(const n of t)e.unshift(na(n))}else r!==void 0&&e.push(na(r));return e}static _$Eu(r,e){const t=e.attribute;return t===!1?void 0:typeof t=="string"?t:typeof r=="string"?r.toLowerCase():void 0}constructor(){super(),this._$Ep=void 0,this.isUpdatePending=!1,this.hasUpdated=!1,this._$Em=null,this._$Ev()}_$Ev(){this._$ES=new Promise(r=>this.enableUpdating=r),this._$AL=new Map,this._$E_(),this.requestUpdate(),this.constructor.l?.forEach(r=>r(this))}addController(r){(this._$EO??=new Set).add(r),this.renderRoot!==void 0&&this.isConnected&&r.hostConnected?.()}removeController(r){this._$EO?.delete(r)}_$E_(){const r=new Map,e=this.constructor.elementProperties;for(const t of e.keys())this.hasOwnProperty(t)&&(r.set(t,this[t]),delete this[t]);r.size>0&&(this._$Ep=r)}createRenderRoot(){const r=this.shadowRoot??this.attachShadow(this.constructor.shadowRootOptions);return le(r,this.constructor.elementStyles),r}connectedCallback(){this.renderRoot??=this.createRenderRoot(),this.enableUpdating(!0),this._$EO?.forEach(r=>r.hostConnected?.())}enableUpdating(r){}disconnectedCallback(){this._$EO?.forEach(r=>r.hostDisconnected?.())}attributeChangedCallback(r,e,t){this._$AK(r,t)}_$ET(r,e){const t=this.constructor.elementProperties.get(r),n=this.constructor._$Eu(r,t);if(n!==void 0&&t.reflect===!0){const i=(t.converter?.toAttribute!==void 0?t.converter:Pr).toAttribute(e,t.type);this._$Em=r,i==null?this.removeAttribute(n):this.setAttribute(n,i),this._$Em=null}}_$AK(r,e){const t=this.constructor,n=t._$Eh.get(r);if(n!==void 0&&this._$Em!==n){const i=t.getPropertyOptions(n),c=typeof i.converter=="function"?{fromAttribute:i.converter}:i.converter?.fromAttribute!==void 0?i.converter:Pr;this._$Em=n,this[n]=c.fromAttribute(e,i.type)??this._$Ej?.get(n)??null,this._$Em=null}}requestUpdate(r,e,t){if(r!==void 0){const n=this.constructor,i=this[r];if(t??=n.getPropertyOptions(r),!((t.hasChanged??ia)(i,e)||t.useDefault&&t.reflect&&i===this._$Ej?.get(r)&&!this.hasAttribute(n._$Eu(r,t))))return;this.C(r,e,t)}this.isUpdatePending===!1&&(this._$ES=this._$EP())}C(r,e,{useDefault:t,reflect:n,wrapped:i},c){t&&!(this._$Ej??=new Map).has(r)&&(this._$Ej.set(r,c??e??this[r]),i!==!0||c!==void 0)||(this._$AL.has(r)||(this.hasUpdated||t||(e=void 0),this._$AL.set(r,e)),n===!0&&this._$Em!==r&&(this._$Eq??=new Set).add(r))}async _$EP(){this.isUpdatePending=!0;try{await this._$ES}catch(e){Promise.reject(e)}const r=this.scheduleUpdate();return r!=null&&await r,!this.isUpdatePending}scheduleUpdate(){return this.performUpdate()}performUpdate(){if(!this.isUpdatePending)return;if(!this.hasUpdated){if(this.renderRoot??=this.createRenderRoot(),this._$Ep){for(const[n,i]of this._$Ep)this[n]=i;this._$Ep=void 0}const t=this.constructor.elementProperties;if(t.size>0)for(const[n,i]of t){const{wrapped:c}=i,s=this[n];c!==!0||this._$AL.has(n)||s===void 0||this.C(n,void 0,i,s)}}let r=!1;const e=this._$AL;try{r=this.shouldUpdate(e),r?(this.willUpdate(e),this._$EO?.forEach(t=>t.hostUpdate?.()),this.update(e)):this._$EM()}catch(t){throw r=!1,this._$EM(),t}r&&this._$AE(e)}willUpdate(r){}_$AE(r){this._$EO?.forEach(e=>e.hostUpdated?.()),this.hasUpdated||(this.hasUpdated=!0,this.firstUpdated(r)),this.updated(r)}_$EM(){this._$AL=new Map,this.isUpdatePending=!1}get updateComplete(){return this.getUpdateComplete()}getUpdateComplete(){return this._$ES}shouldUpdate(r){return!0}update(r){this._$Eq&&=this._$Eq.forEach(e=>this._$ET(e,this[e])),this._$EM()}updated(r){}firstUpdated(r){}}K.elementStyles=[],K.shadowRootOptions={mode:"open"},K[nr("elementProperties")]=new Map,K[nr("finalized")]=new Map,me?.({ReactiveElement:K}),(gr.reactiveElementVersions??=[]).push("2.1.0");/**
 * @license
 * Copyright 2017 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */const Mr=globalThis,fr=Mr.trustedTypes,la=fr?fr.createPolicy("lit-html",{createHTML:a=>a}):void 0,Or="$lit$",H=`lit$${Math.random().toFixed(9).slice(2)}$`,Br="?"+H,ge=`<${Br}>`,Y=document,or=()=>Y.createComment(""),ir=a=>a===null||typeof a!="object"&&typeof a!="function",Hr=Array.isArray,sa=a=>Hr(a)||typeof a?.[Symbol.iterator]=="function",Vr=`[ 	
\f\r]`,cr=/<(?:(!--|\/[^a-zA-Z])|(\/?[a-zA-Z][^>\s]*)|(\/?$))/g,da=/-->/g,pa=/>/g,Z=RegExp(`>|${Vr}(?:([^\\s"'>=/]+)(${Vr}*=${Vr}*(?:[^ 	
\f\r"'\`<>=]|("|')|))|$)`,"g"),va=/'/g,ha=/"/g,ua=/^(?:script|style|textarea|title)$/i,Nr=a=>(r,...e)=>({_$litType$:a,strings:r,values:e}),ho=Nr(1),uo=Nr(2),bo=Nr(3),G=Symbol.for("lit-noChange"),z=Symbol.for("lit-nothing"),ba=new WeakMap,X=Y.createTreeWalker(Y,129);function ma(a,r){if(!Hr(a)||!a.hasOwnProperty("raw"))throw Error("invalid template strings array");return la!==void 0?la.createHTML(r):r}const ga=(a,r)=>{const e=a.length-1,t=[];let n,i=r===2?"<svg>":r===3?"<math>":"",c=cr;for(let s=0;s<e;s++){const l=a[s];let d,p,v=-1,u=0;for(;u<l.length&&(c.lastIndex=u,p=c.exec(l),p!==null);)u=c.lastIndex,c===cr?p[1]==="!--"?c=da:p[1]!==void 0?c=pa:p[2]!==void 0?(ua.test(p[2])&&(n=RegExp("</"+p[2],"g")),c=Z):p[3]!==void 0&&(c=Z):c===Z?p[0]===">"?(c=n??cr,v=-1):p[1]===void 0?v=-2:(v=c.lastIndex-p[2].length,d=p[1],c=p[3]===void 0?Z:p[3]==='"'?ha:va):c===ha||c===va?c=Z:c===da||c===pa?c=cr:(c=Z,n=void 0);const f=c===Z&&a[s+1].startsWith("/>")?" ":"";i+=c===cr?l+ge:v>=0?(t.push(d),l.slice(0,v)+Or+l.slice(v)+H+f):l+H+(v===-2?s:f)}return[ma(a,i+(a[e]||"<?>")+(r===2?"</svg>":r===3?"</math>":"")),t]};class lr{constructor({strings:r,_$litType$:e},t){let n;this.parts=[];let i=0,c=0;const s=r.length-1,l=this.parts,[d,p]=ga(r,e);if(this.el=lr.createElement(d,t),X.currentNode=this.el.content,e===2||e===3){const v=this.el.content.firstChild;v.replaceWith(...v.childNodes)}for(;(n=X.nextNode())!==null&&l.length<s;){if(n.nodeType===1){if(n.hasAttributes())for(const v of n.getAttributeNames())if(v.endsWith(Or)){const u=p[c++],f=n.getAttribute(v).split(H),k=/([.?@])?(.*)/.exec(u);l.push({type:1,index:i,name:k[2],strings:f,ctor:k[1]==="."?xa:k[1]==="?"?ya:k[1]==="@"?wa:sr}),n.removeAttribute(v)}else v.startsWith(H)&&(l.push({type:6,index:i}),n.removeAttribute(v));if(ua.test(n.tagName)){const v=n.textContent.split(H),u=v.length-1;if(u>0){n.textContent=fr?fr.emptyScript:"";for(let f=0;f<u;f++)n.append(v[f],or()),X.nextNode(),l.push({type:2,index:++i});n.append(v[u],or())}}}else if(n.nodeType===8)if(n.data===Br)l.push({type:2,index:i});else{let v=-1;for(;(v=n.data.indexOf(H,v+1))!==-1;)l.push({type:7,index:i}),v+=H.length-1}i++}}static createElement(r,e){const t=Y.createElement("template");return t.innerHTML=r,t}}function q(a,r,e=a,t){if(r===G)return r;let n=t!==void 0?e._$Co?.[t]:e._$Cl;const i=ir(r)?void 0:r._$litDirective$;return n?.constructor!==i&&(n?._$AO?.(!1),i===void 0?n=void 0:(n=new i(a),n._$AT(a,e,t)),t!==void 0?(e._$Co??=[])[t]=n:e._$Cl=n),n!==void 0&&(r=q(a,n._$AS(a,r.values),n,t)),r}class fa{constructor(r,e){this._$AV=[],this._$AN=void 0,this._$AD=r,this._$AM=e}get parentNode(){return this._$AM.parentNode}get _$AU(){return this._$AM._$AU}u(r){const{el:{content:e},parts:t}=this._$AD,n=(r?.creationScope??Y).importNode(e,!0);X.currentNode=n;let i=X.nextNode(),c=0,s=0,l=t[0];for(;l!==void 0;){if(c===l.index){let d;l.type===2?d=new J(i,i.nextSibling,this,r):l.type===1?d=new l.ctor(i,l.name,l.strings,this,r):l.type===6&&(d=new ka(i,this,r)),this._$AV.push(d),l=t[++s]}c!==l?.index&&(i=X.nextNode(),c++)}return X.currentNode=Y,n}p(r){let e=0;for(const t of this._$AV)t!==void 0&&(t.strings!==void 0?(t._$AI(r,t,e),e+=t.strings.length-2):t._$AI(r[e])),e++}}class J{get _$AU(){return this._$AM?._$AU??this._$Cv}constructor(r,e,t,n){this.type=2,this._$AH=z,this._$AN=void 0,this._$AA=r,this._$AB=e,this._$AM=t,this.options=n,this._$Cv=n?.isConnected??!0}get parentNode(){let r=this._$AA.parentNode;const e=this._$AM;return e!==void 0&&r?.nodeType===11&&(r=e.parentNode),r}get startNode(){return this._$AA}get endNode(){return this._$AB}_$AI(r,e=this){r=q(this,r,e),ir(r)?r===z||r==null||r===""?(this._$AH!==z&&this._$AR(),this._$AH=z):r!==this._$AH&&r!==G&&this._(r):r._$litType$!==void 0?this.$(r):r.nodeType!==void 0?this.T(r):sa(r)?this.k(r):this._(r)}O(r){return this._$AA.parentNode.insertBefore(r,this._$AB)}T(r){this._$AH!==r&&(this._$AR(),this._$AH=this.O(r))}_(r){this._$AH!==z&&ir(this._$AH)?this._$AA.nextSibling.data=r:this.T(Y.createTextNode(r)),this._$AH=r}$(r){const{values:e,_$litType$:t}=r,n=typeof t=="number"?this._$AC(r):(t.el===void 0&&(t.el=lr.createElement(ma(t.h,t.h[0]),this.options)),t);if(this._$AH?._$AD===n)this._$AH.p(e);else{const i=new fa(n,this),c=i.u(this.options);i.p(e),this.T(c),this._$AH=i}}_$AC(r){let e=ba.get(r.strings);return e===void 0&&ba.set(r.strings,e=new lr(r)),e}k(r){Hr(this._$AH)||(this._$AH=[],this._$AR());const e=this._$AH;let t,n=0;for(const i of r)n===e.length?e.push(t=new J(this.O(or()),this.O(or()),this,this.options)):t=e[n],t._$AI(i),n++;n<e.length&&(this._$AR(t&&t._$AB.nextSibling,n),e.length=n)}_$AR(r=this._$AA.nextSibling,e){for(this._$AP?.(!1,!0,e);r&&r!==this._$AB;){const t=r.nextSibling;r.remove(),r=t}}setConnected(r){this._$AM===void 0&&(this._$Cv=r,this._$AP?.(r))}}class sr{get tagName(){return this.element.tagName}get _$AU(){return this._$AM._$AU}constructor(r,e,t,n,i){this.type=1,this._$AH=z,this._$AN=void 0,this.element=r,this.name=e,this._$AM=n,this.options=i,t.length>2||t[0]!==""||t[1]!==""?(this._$AH=Array(t.length-1).fill(new String),this.strings=t):this._$AH=z}_$AI(r,e=this,t,n){const i=this.strings;let c=!1;if(i===void 0)r=q(this,r,e,0),c=!ir(r)||r!==this._$AH&&r!==G,c&&(this._$AH=r);else{const s=r;let l,d;for(r=i[0],l=0;l<i.length-1;l++)d=q(this,s[t+l],e,l),d===G&&(d=this._$AH[l]),c||=!ir(d)||d!==this._$AH[l],d===z?r=z:r!==z&&(r+=(d??"")+i[l+1]),this._$AH[l]=d}c&&!n&&this.j(r)}j(r){r===z?this.element.removeAttribute(this.name):this.element.setAttribute(this.name,r??"")}}class xa extends sr{constructor(){super(...arguments),this.type=3}j(r){this.element[this.name]=r===z?void 0:r}}class ya extends sr{constructor(){super(...arguments),this.type=4}j(r){this.element.toggleAttribute(this.name,!!r&&r!==z)}}class wa extends sr{constructor(r,e,t,n,i){super(r,e,t,n,i),this.type=5}_$AI(r,e=this){if((r=q(this,r,e,0)??z)===G)return;const t=this._$AH,n=r===z&&t!==z||r.capture!==t.capture||r.once!==t.once||r.passive!==t.passive,i=r!==z&&(t===z||n);n&&this.element.removeEventListener(this.name,this,t),i&&this.element.addEventListener(this.name,this,r),this._$AH=r}handleEvent(r){typeof this._$AH=="function"?this._$AH.call(this.options?.host??this.element,r):this._$AH.handleEvent(r)}}class ka{constructor(r,e,t){this.element=r,this.type=6,this._$AN=void 0,this._$AM=e,this.options=t}get _$AU(){return this._$AM._$AU}_$AI(r){q(this,r)}}const mo={M:Or,P:H,A:Br,C:1,L:ga,R:fa,D:sa,V:q,I:J,H:sr,N:ya,U:wa,B:xa,F:ka},fe=Mr.litHtmlPolyfillSupport;fe?.(lr,J),(Mr.litHtmlVersions??=[]).push("3.3.0");const xe=(a,r,e)=>{const t=e?.renderBefore??r;let n=t._$litPart$;if(n===void 0){const i=e?.renderBefore??null;t._$litPart$=n=new J(r.insertBefore(or(),i),i,void 0,e??{})}return n._$AI(a),n};/**
 * @license
 * Copyright 2017 Google LLC
 * SPDX-License-Identifier: BSD-3-Clause
 */const Ur=globalThis;class xr extends K{constructor(){super(...arguments),this.renderOptions={host:this},this._$Do=void 0}createRenderRoot(){const r=super.createRenderRoot();return this.renderOptions.renderBefore??=r.firstChild,r}update(r){const e=this.render();this.hasUpdated||(this.renderOptions.isConnected=this.isConnected),super.update(r),this._$Do=xe(e,this.renderRoot,this.renderOptions)}connectedCallback(){super.connectedCallback(),this._$Do?.setConnected(!0)}disconnectedCallback(){super.disconnectedCallback(),this._$Do?.setConnected(!1)}render(){return G}}xr._$litElement$=!0,xr.finalized=!0,Ur.litElementHydrateSupport?.({LitElement:xr});const ye=Ur.litElementPolyfillSupport;ye?.({LitElement:xr});const go={_$AK:(a,r,e)=>{a._$AK(r,e)},_$AL:a=>a._$AL};(Ur.litElementVersions??=[]).push("4.2.0");const Aa={};function x(a,r){if(typeof document<"u"&&r?.styleSheet){const e=Aa[a];Aa[a]=r.styleSheet,document.adoptedStyleSheets=[...document.adoptedStyleSheets.filter(t=>t!==e),r.styleSheet]}}const we=a=>a?.trim().charAt(0)??void 0,ke=(a,r)=>{let e=0;const t=Math.min(a.length,50);for(let n=0;n<t;n++)e=(e+a.charCodeAt(n))%(r??33);return e},Ae=(a,r,e)=>r?.length?a?.trim()===""?e||r[0]:r[ke(a,r.length)]||e||r[0]:e,za=h(Ir,["blue","orange","mint","red","yellow","green","maroon","purple"]),ze=h(Ir,["neutral","white","black"]),yr=h(_,["lg","md","sm","xs"]),Ce=[...yr,"profile-sm","profile-md","profile-lg"],Se=yr,_e=yr,Ee=yr,Ca=m`
/* This rule allows Avatar to acclimate to its parental size controls  */
--arc-modifier: inherit;
--arc-internal-avatar-num-size-1: inherit;
--arc-internal-avatar-num-size-2: inherit;
--arc-internal-avatar-num-size-3: inherit;
--arc-internal-avatar-size: inherit;
`,Fr=m`

&[data-size=xs] {
  --arc-modifier: 10; /* unitless, desired size at 100% */
  --arc-internal-avatar-num-size-1: 10;
  --arc-internal-avatar-num-size-2: 8;
  --arc-internal-avatar-num-size-3: 6;
  --arc-internal-avatar-size: 20;
}

&[data-size=sm] {
  --arc-modifier: 12; /* unitless, desired size at 100% */
  --arc-internal-avatar-num-size-1: 12;
  --arc-internal-avatar-num-size-2: 10;
  --arc-internal-avatar-num-size-3: 8;
  --arc-internal-avatar-size: 24;
}

&:is([data-size=md], [data-size=inherit], :not([data-size])) { /* @default size */
  --arc-modifier: 14; /* unitless, desired size at 100% */
  --arc-internal-avatar-num-size-1: 14;
  --arc-internal-avatar-num-size-2: 12;
  --arc-internal-avatar-num-size-3: 10;
  --arc-internal-avatar-size: 32;
}

&[data-size=lg] {
  --arc-modifier: 16; /* unitless, desired size at 100% */
  --arc-internal-avatar-num-size-1: 16;
  --arc-internal-avatar-num-size-2: 14;
  --arc-internal-avatar-num-size-3: 12;
  --arc-internal-avatar-size: 40;
}

`,je=m`
@layer arc-components {

[arc-avatar] {

  /* Colors --------------------------------------------------------• */

  --arc-internal-avatar-color: var(--arc-avatar-color, var(--arc-color-text-inverse));

  &:has([data-color=blue]) { /* @default color */
    --arc-internal-avatar-background: var(--arc-color-blue-400);
  }
  &:has([data-color=purple]) {
    --arc-internal-avatar-background: var(--arc-color-purple-400);
  }
  &:has([data-color=red]) {
    --arc-internal-avatar-background: var(--arc-color-red-400);
  }
  &:has([data-color=yellow]) {
    --arc-internal-avatar-background: var(--arc-color-yellow-400);
  }
  &:has([data-color=green]) {
    --arc-internal-avatar-background: var(--arc-color-green-400);
  }
  &:has([data-color=maroon]) {
    --arc-internal-avatar-background: var(--arc-color-maroon-400);
  }
  &:has([data-color=orange]) {
    --arc-internal-avatar-background: var(--arc-color-orange-400);
  }
  &:has([data-color=mint]) {
    --arc-internal-avatar-background: var(--arc-color-mint-400);
  }


  /* RESERVED COLORS -----------------------------------------------• */

  &:has([data-color=black]) {
    --arc-internal-avatar-background: var(--arc-color-mono-black);
  }

  &:has([data-color=neutral]) { /* @default anonymous */
    --arc-internal-avatar-color: var(--arc-internal-neutral-color, var(--arc-color-icon-secondary));
    --arc-internal-avatar-background: var(--arc-internal-neutral-background, var(--arc-color-alpha-black-10));
  }

  &:has([data-color=white]) {
    --arc-internal-avatar-color: var(--arc-color-text-primary);
    --arc-internal-avatar-background: var(--arc-color-mono-white);
  }


  /* SIZES ---------------------------------------------------------• */

  ${Fr}


  /* PROFILE SIZES -------------------------------------------------• */

  &[data-size=profile-sm] {
    --arc-modifier: 28; /* unitless, desired size at 100% */
    --arc-internal-avatar-size: 64;
  }

  &[data-size=profile-md] {
    --arc-modifier: 32; /* unitless, desired size at 100% */
    --arc-internal-avatar-size: 96;
  }

  &[data-size=profile-lg] {
    --arc-modifier: 40; /* unitless, desired size at 100% */
    --arc-internal-avatar-size: 160;
  }


  /* COMPUTED -------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-avatar-font-size: calc(var(--arc-modifier-px));
    --arc-computed-avatar-size: calc(var(--arc-internal-avatar-size) / var(--arc-modifier-em));
    --arc-computed-avatar-num-size-1: calc(var(--arc-internal-avatar-num-size-1) / var(--arc-modifier-em));
    --arc-computed-avatar-num-size-2: calc(var(--arc-internal-avatar-num-size-2) / var(--arc-modifier-em));
    --arc-computed-avatar-num-size-3: calc(var(--arc-internal-avatar-num-size-3) / var(--arc-modifier-em));
  }
}

/* CORE AVATAR STYLES ----------------------------------------------• */

[arc-avatar] {
  font-size: var(--arc-avatar-font-size, var(--arc-computed-avatar-font-size));

  flex: none;
  align-self: center;

  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: var(--arc-computed-avatar-size);
  aspect-ratio: 1 / 1;
  line-height: normal;

  text-align: center;
  position: relative;
  overflow: hidden;
  border-radius: var(--arc-border-radius-circle);

  &[data-is-inactive] {
    opacity: var(--arc-alpha-50);
  }

  [data-num-length] {
    font-size: var(--arc-computed-avatar-num-size-1);
  }

  [data-num-length='2'] {
    font-size: var(--arc-computed-avatar-num-size-2);
  }

  [data-num-length='3'] {
    font-size: var(--arc-computed-avatar-num-size-3);
  }


  /* INNER ELEMENTS ------------------------------------------------• */

  [data-element=counter] {
    --arc-internal-avatar-pointer-events: all;

    /* overrides ArcButton style properties */
    /** @todo provide css vars to ArcButton styles */
    padding: var(--arc-space-0);
    background: var(--arc-avatar-background, var(--arc-internal-avatar-background));
    color: var(--arc-avatar-color, var(--arc-internal-avatar-color));

    &:disabled {
      cursor: default;
    }
  }

  [data-element=image] {
    object-fit: cover;

    /* Hides problematic image */
    &:is([data-not-loaded], [data-has-error]) {
      --arc-internal-avatar-visibility: hidden;
    }

    /* Hides subsequent elements with valid image */
    &:not([data-has-error]) ~ [data-element] {
      --arc-internal-avatar-color: transparent;
      --arc-internal-avatar-background: none;
    }
  }

  :is(
    [data-element=counter],
    [data-element=initial],
    [data-element=anonymous]
   ) {
    background: var(--arc-internal-avatar-background);
    color: var(--arc-internal-avatar-color);
  }

  [data-element] {
    flex: none;
    position: absolute;
    inset: 0;
    display: flex;
    align-items: center;
    justify-content: center;

    width: 100%;
    height: 100%;

    border-radius: inherit;
    font-weight: var(--arc-font-weight-600);
    visibility: var(--arc-internal-avatar-visibility, unset);

    transition-property: visibility, background-color, color;
    transition-behavior: allow-discrete;
    transition-duration: 200ms;
    transition-timing-function: linear;

    pointer-events: var(--arc-internal-avatar-pointer-events, none);
    user-select: none;
    -webkit-user-select: none;
  }
}

}
`,$e=({children:a,initial:r,color:e,...t})=>(0,o.jsx)("span",{...t,"data-element":"initial","data-color":e,children:a||r}),Te=({isLoaded:a,hasError:r,onLoad:e,onError:t,...n})=>{const i=!a&&""||void 0,c=r&&""||void 0,s=!a||r||void 0;return(0,o.jsx)("img",{...n,hidden:s,"data-element":"image","data-not-loaded":i,"data-has-error":c,onLoad:e,onError:t})},Sa=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M16.2362 2V4.04215H3.7443V2H16.2362ZM13.8453 11.7538C13.8453 9.47528 12.1743 7.75773 9.96905 7.75773C7.79316 7.75773 6.15147 9.47528 6.15147 11.7538C6.15147 14.0323 7.7899 15.753 9.96905 15.753C12.1775 15.753 13.8453 14.0355 13.8453 11.7569V11.7538ZM9.96905 18C6.29153 18 3.5 15.3213 3.5 11.7538C3.5 8.20209 6.27524 5.50758 9.96905 5.49183C13.6922 5.49183 16.5 8.17057 16.5 11.738V17.6754H13.8485V16.3581C13.8485 16.3581 12.3925 18 9.97231 18H9.96905Z",fill:"var(--arc-color-logo-articulate, #000000)"})}),_a=a=>(0,o.jsxs)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:[(0,o.jsx)("defs",{children:(0,o.jsxs)("linearGradient",{id:"ai_gradient",x1:"2",y1:"18",x2:"18",y2:"2",gradientUnits:"userSpaceOnUse",children:[(0,o.jsx)("stop",{stopColor:"var(--arc-color-logo-ai-purple, #9c15ff)"}),(0,o.jsx)("stop",{offset:"1",stopColor:"var(--arc-color-logo-ai-blue, #1eb4fb)"})]})}),(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M17.25 10C17.25 14.0041 14.0041 17.25 10 17.25C5.99594 17.25 2.75 14.0041 2.75 10C2.75 5.99594 5.99594 2.75 10 2.75C14.0041 2.75 17.25 5.99594 17.25 10ZM18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10ZM11.8524 12.87V8.17199H12.6444V12.87H11.8524ZM11.8524 7.28999V6.29999H12.6444V7.28999H11.8524ZM6.5 11.511C6.5 11.211 6.584 10.953 6.752 10.737C6.926 10.515 7.163 10.344 7.463 10.224C7.763 10.104 8.111 10.044 8.507 10.044C8.717 10.044 8.939 10.062 9.173 10.098C9.407 10.128 9.614 10.176 9.794 10.242V9.89999C9.794 9.53999 9.686 9.25799 9.47 9.05399C9.254 8.84399 8.948 8.73899 8.552 8.73899C8.294 8.73899 8.045 8.78699 7.805 8.88299C7.571 8.97299 7.322 9.10499 7.058 9.27899L6.77 8.72099C7.076 8.51099 7.382 8.35499 7.688 8.25299C7.994 8.14499 8.312 8.09099 8.642 8.09099C9.242 8.09099 9.716 8.25899 10.064 8.59499C10.412 8.92499 10.586 9.38699 10.586 9.98099V11.97C10.586 12.066 10.604 12.138 10.64 12.186C10.682 12.228 10.748 12.252 10.838 12.258V12.87C10.76 12.882 10.691 12.891 10.631 12.897C10.577 12.903 10.532 12.906 10.496 12.906C10.31 12.906 10.169 12.855 10.073 12.753C9.983 12.651 9.932 12.543 9.92 12.429L9.902 12.132C9.698 12.396 9.431 12.6 9.101 12.744C8.771 12.888 8.444 12.96 8.12 12.96C7.808 12.96 7.529 12.897 7.283 12.771C7.037 12.639 6.845 12.465 6.707 12.249C6.569 12.027 6.5 11.781 6.5 11.511ZM9.56 11.844C9.632 11.76 9.689 11.676 9.731 11.592C9.773 11.502 9.794 11.427 9.794 11.367V10.782C9.608 10.71 9.413 10.656 9.209 10.62C9.005 10.578 8.804 10.557 8.606 10.557C8.204 10.557 7.877 10.638 7.625 10.8C7.379 10.956 7.256 11.172 7.256 11.448C7.256 11.598 7.295 11.745 7.373 11.889C7.457 12.027 7.577 12.141 7.733 12.231C7.895 12.321 8.093 12.366 8.327 12.366C8.573 12.366 8.807 12.318 9.029 12.222C9.251 12.12 9.428 11.994 9.56 11.844Z",fill:"url(#ai_gradient)"})]}),Ea=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M10 18C5.582 18 2 14.418 2 10C2 5.582 5.582 2 10 2C14.418 2 18 5.582 18 10C18 14.418 14.418 18 10 18ZM10 17.272C14.016 17.272 17.272 14.016 17.272 10C17.272 5.984 14.016 2.728 10 2.728C5.984 2.728 2.728 5.984 2.728 10C2.728 14.016 5.984 17.272 10 17.272ZM11.3493 8.65067L13.3747 7.51C13.4276 7.48023 13.4874 7.46481 13.5481 7.46529C13.6088 7.46576 13.6683 7.48211 13.7207 7.51271C13.7732 7.54331 13.8167 7.58709 13.847 7.63971C13.8772 7.69233 13.8932 7.75196 13.8933 7.81267V12.14C13.8934 12.2014 13.8772 12.2616 13.8464 12.3147C13.8156 12.3678 13.7713 12.4118 13.7181 12.4423C13.6648 12.4727 13.6044 12.4886 13.5431 12.4882C13.4817 12.4878 13.4215 12.4712 13.3687 12.44L11.3433 11.2453C11.2911 11.2146 11.2479 11.1707 11.2178 11.1181C11.1878 11.0655 11.172 11.0059 11.172 10.9453V8.95333C11.172 8.828 11.24 8.712 11.3493 8.65067ZM7.20267 7.46333H9.93867C10.3227 7.46333 10.634 7.77467 10.634 8.15867V11.592C10.634 11.9767 10.3227 12.288 9.93867 12.288H7.202C7.0177 12.288 6.84094 12.2148 6.71056 12.0846C6.58018 11.9543 6.50684 11.7776 6.50667 11.5933V8.16C6.50667 7.77533 6.818 7.464 7.202 7.464L7.20267 7.46333Z",fill:"var(--arc-color-logo-peek, #497add)"})}),ja=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M17.25 10C17.25 14.0041 14.0041 17.25 10 17.25C5.99594 17.25 2.75 14.0041 2.75 10C2.75 5.99594 5.99594 2.75 10 2.75C14.0041 2.75 17.25 5.99594 17.25 10ZM18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10ZM11.7343 12.569C11.3803 12.569 11.0563 12.506 10.7623 12.38C10.4743 12.248 10.2223 12.071 10.0063 11.849C9.79627 11.621 9.63127 11.36 9.51127 11.066C9.39727 10.772 9.34027 10.46 9.34027 10.13C9.34027 9.68601 9.44227 9.28101 9.64627 8.91501C9.85027 8.54901 10.1323 8.25501 10.4923 8.03301C10.8583 7.81101 11.2753 7.70001 11.7433 7.70001C12.2173 7.70001 12.6283 7.81401 12.9763 8.04201C13.3303 8.26401 13.6033 8.55801 13.7953 8.92401C13.9933 9.28401 14.0923 9.67401 14.0923 10.094C14.0923 10.154 14.0893 10.214 14.0833 10.274C14.0833 10.328 14.0803 10.37 14.0743 10.4H10.1773C10.2013 10.706 10.2853 10.979 10.4293 11.219C10.5793 11.453 10.7713 11.639 11.0053 11.777C11.2393 11.909 11.4913 11.975 11.7613 11.975C12.0493 11.975 12.3193 11.903 12.5713 11.759C12.8293 11.615 13.0063 11.426 13.1023 11.192L13.7863 11.381C13.6843 11.609 13.5313 11.813 13.3273 11.993C13.1293 12.173 12.8923 12.314 12.6163 12.416C12.3463 12.518 12.0523 12.569 11.7343 12.569ZM10.1503 9.85101H13.3543C13.3303 9.54501 13.2433 9.27501 13.0933 9.04101C12.9433 8.80701 12.7513 8.62401 12.5173 8.49201C12.2833 8.36001 12.0253 8.29401 11.7433 8.29401C11.4673 8.29401 11.2123 8.36001 10.9783 8.49201C10.7443 8.62401 10.5523 8.80701 10.4023 9.04101C10.2583 9.27501 10.1743 9.54501 10.1503 9.85101ZM8.984 8.47401C8.588 8.48001 8.237 8.58201 7.931 8.78001C7.631 8.97201 7.418 9.23901 7.292 9.58101V12.479H6.5V7.78101H7.238V8.87001C7.4 8.54601 7.613 8.28501 7.877 8.08701C8.147 7.88301 8.429 7.76901 8.723 7.74501H8.876C8.918 7.74501 8.954 7.74801 8.984 7.75401V8.47401Z",fill:"var(--arc-color-logo-reach, #0081de)"})}),$a=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M17.25 10C17.25 14.0041 14.0041 17.25 10 17.25C5.99594 17.25 2.75 14.0041 2.75 10C2.75 5.99594 5.99594 2.75 10 2.75C14.0041 2.75 17.25 5.99594 17.25 10ZM18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10ZM9.38402 8.46899C8.98802 8.47499 8.63702 8.57699 8.33102 8.77499C8.03102 8.96699 7.81802 9.23399 7.69202 9.57599V12.474H6.90002V7.77599H7.63802V8.86499C7.80002 8.54099 8.01302 8.27999 8.27702 8.08199C8.54702 7.87799 8.82902 7.76399 9.12302 7.73999H9.27602C9.31802 7.73999 9.35402 7.74299 9.38402 7.74899V8.46899ZM11.5195 12.474L9.67455 7.77599H10.5025L11.9785 11.718L13.4545 7.77599H14.2285L12.3835 12.474H11.5195Z",fill:"var(--arc-color-logo-review, #35b062)"})}),Ta=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M10 18C5.582 18 2 14.418 2 10C2 5.582 5.582 2 10 2C14.418 2 18 5.582 18 10C18 14.418 14.418 18 10 18ZM10 17.272C14.016 17.272 17.272 14.016 17.272 10C17.272 5.984 14.016 2.728 10 2.728C5.984 2.728 2.728 5.984 2.728 10C2.728 14.016 5.984 17.272 10 17.272ZM9.29467 8.48267C8.90667 8.49467 8.564 8.59467 8.268 8.78267C7.97067 8.97067 7.76067 9.232 7.63733 9.56733V12.406H6.86133V7.804H7.584V8.87067C7.74267 8.55333 7.95133 8.296 8.21067 8.09933C8.46867 7.90267 8.74467 7.79267 9.03867 7.76867H9.18867C9.23 7.76867 9.26533 7.772 9.29467 7.77733V8.48267ZM11.6747 12.494C11.3035 12.4941 10.9347 12.4363 10.5813 12.3227C10.2233 12.208 9.91467 12.042 9.656 11.824L9.99067 11.304C10.2613 11.51 10.532 11.664 10.802 11.7667C11.0727 11.87 11.3573 11.9213 11.6573 11.9213C11.992 11.9213 12.258 11.8547 12.4553 11.7227C12.5492 11.6632 12.6258 11.5803 12.6776 11.482C12.7294 11.3838 12.7546 11.2737 12.7507 11.1627C12.7507 11.0513 12.724 10.956 12.6707 10.876C12.6092 10.7892 12.5278 10.7183 12.4333 10.6693C12.3044 10.6014 12.1682 10.5482 12.0273 10.5107C11.8627 10.464 11.672 10.4133 11.454 10.3607C11.2142 10.3005 10.9763 10.2329 10.7407 10.158C10.566 10.1052 10.3997 10.028 10.2467 9.92867C10.1259 9.85011 10.0285 9.7405 9.96467 9.61133C9.90168 9.46686 9.87162 9.31019 9.87667 9.15267C9.87261 8.94053 9.91923 8.73049 10.0127 8.54C10.104 8.36067 10.2287 8.21133 10.388 8.09067C10.5561 7.96556 10.746 7.87287 10.948 7.81733C11.173 7.7542 11.4057 7.72301 11.6393 7.72467C11.986 7.72467 12.3093 7.78067 12.6093 7.892C12.9093 8.004 13.162 8.15067 13.3673 8.33267L13.0147 8.8C12.824 8.63151 12.6026 8.50136 12.3627 8.41667C12.1249 8.33193 11.8744 8.28864 11.622 8.28867C11.34 8.28867 11.1 8.34867 10.9033 8.46933C10.7067 8.59 10.608 8.78267 10.608 9.04733C10.608 9.15267 10.6273 9.24067 10.6653 9.31133C10.7106 9.38903 10.776 9.45306 10.8547 9.49667C10.9433 9.55 11.056 9.59667 11.1947 9.638C11.3327 9.67867 11.4987 9.72333 11.6927 9.77C11.998 9.84067 12.264 9.91267 12.4907 9.986C12.7167 10.0593 12.9047 10.146 13.0547 10.246C13.2047 10.346 13.316 10.4633 13.3893 10.5993C13.4627 10.734 13.5 10.8987 13.5 11.0927C13.5 11.5213 13.3353 11.8627 13.0067 12.1153C12.6767 12.368 12.2333 12.494 11.6747 12.494Z",fill:"var(--arc-color-logo-rise, #ff6600)"})}),Ia=a=>(0,o.jsx)("svg",{...a,viewBox:"0 0 20 20",xmlns:"http://www.w3.org/2000/svg",children:(0,o.jsx)("path",{fillRule:"evenodd",clipRule:"evenodd",d:"M17.25 10C17.25 14.0041 14.0041 17.25 10 17.25C5.99594 17.25 2.75 14.0041 2.75 10C2.75 5.99594 5.99594 2.75 10 2.75C14.0041 2.75 17.25 5.99594 17.25 10ZM18 10C18 14.4183 14.4183 18 10 18C5.58172 18 2 14.4183 2 10C2 5.58172 5.58172 2 10 2C14.4183 2 18 5.58172 18 10ZM11.2815 6.29999H12.0735V11.655C12.0735 11.913 12.1095 12.072 12.1815 12.132C12.2535 12.192 12.3405 12.222 12.4425 12.222C12.5685 12.222 12.6885 12.21 12.8025 12.186C12.9165 12.156 13.0125 12.126 13.0905 12.096L13.2165 12.744C13.0845 12.798 12.9285 12.843 12.7485 12.879C12.5685 12.915 12.4095 12.933 12.2715 12.933C11.9655 12.933 11.7225 12.846 11.5425 12.672C11.3685 12.498 11.2815 12.255 11.2815 11.943V6.29999ZM8.48 12.96C8.09 12.96 7.724 12.897 7.382 12.771C7.04 12.639 6.746 12.444 6.5 12.186L6.806 11.628C7.076 11.88 7.346 12.06 7.616 12.168C7.886 12.276 8.162 12.33 8.444 12.33C8.768 12.33 9.032 12.27 9.236 12.15C9.44 12.024 9.542 11.841 9.542 11.601C9.542 11.427 9.488 11.298 9.38 11.214C9.278 11.13 9.131 11.061 8.939 11.007C8.747 10.947 8.519 10.881 8.255 10.809C7.931 10.713 7.658 10.611 7.436 10.503C7.214 10.395 7.046 10.26 6.932 10.098C6.818 9.93599 6.761 9.73199 6.761 9.48599C6.761 9.18599 6.839 8.93399 6.995 8.72999C7.151 8.51999 7.364 8.36099 7.634 8.25299C7.91 8.14499 8.222 8.09099 8.57 8.09099C8.918 8.09099 9.233 8.14499 9.515 8.25299C9.797 8.36099 10.04 8.51999 10.244 8.72999L9.884 9.27899C9.698 9.08699 9.494 8.94599 9.272 8.85599C9.05 8.75999 8.801 8.71199 8.525 8.71199C8.363 8.71199 8.204 8.73299 8.048 8.77499C7.892 8.81099 7.763 8.87999 7.661 8.98199C7.559 9.07799 7.508 9.21599 7.508 9.39599C7.508 9.54599 7.547 9.66599 7.625 9.75599C7.709 9.83999 7.829 9.91199 7.985 9.97199C8.147 10.032 8.342 10.098 8.57 10.17C8.924 10.272 9.23 10.374 9.488 10.476C9.752 10.578 9.956 10.71 10.1 10.872C10.244 11.034 10.316 11.259 10.316 11.547C10.316 11.991 10.148 12.339 9.812 12.591C9.476 12.837 9.032 12.96 8.48 12.96Z",fill:"var(--arc-color-logo-storyline, #c560b3)"})}),Ie=m`
@layer arc-components {

[arc-icon] {
  font-size: var(--arc-icon-size, 1em);
  color: var(--arc-icon-color, currentcolor);
  opacity: var(--arc-icon-opacity, 1);

  &:not([hidden]) {
    --fa-display: var(--arc-icon-display, inline-flex);

    flex: none;
    display: var(--fa-display);
    align-items: center;
    justify-content: center;
    max-height: 1lh;
    line-height: 1lh;
  }

  &:has(svg) {
    width: 1em;
    height: var(--arc-icon-svg-height, 1lh);
    align-self: var(--arc-icon-svg-align-self, center);
    vertical-align: -0.1lh;

    svg {
      fill: var(--arc-icon-color, currentcolor);
      width: 100%; /* keeps Safari happy ¯\_(ツ)_/¯ */
    }
  }

  [class|=fa] {
    align-items: center;
    justify-content: center;
    line-height: var(--arc-icon-fa-line-height, 1) !important; /* fix for FontAwesome line-height oddities in certain cases */
  }
}

}
`,Re=a=>typeof a=="number"?`calc(${a/10}rem * var(--arc-internal-font-size))`:a,w=({children:a,icon:r="fa-regular fa-circle-dashed",color:e,size:t,style:n,...i})=>{const c={...n,"--arc-icon-size":Re(t),"--arc-icon-color":e},s=!!(i["aria-label"]||i["aria-labelledby"]),l=typeof r=="string"&&r.length>0?(0,o.jsx)("span",{className:r}):r;return(0,o.jsx)("span",{"aria-hidden":!s||void 0,...i,"arc-icon":"",style:c,children:a||l})};w.Articulate=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(Sa,{})}),w.AI=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(_a,{})}),w.Peek=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(Ea,{})}),w.Reach=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(ja,{})}),w.Review=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)($a,{})}),w.Rise=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(Ta,{})}),w.Storyline=a=>(0,o.jsx)(w,{...a,icon:(0,o.jsx)(Ia,{})}),x("ArcIcon",Ie);const Le=({children:a,icon:r,color:e,...t})=>(0,o.jsx)("span",{...t,"data-element":"anonymous","data-color":"neutral",children:a||(0,o.jsx)(w,{icon:r})}),Ra="blue",La="neutral",Pe="fa-solid fa-user",Dr=(0,g.forwardRef)(({children:a,size:r,color:e,isActive:t=!0,isAnonymous:n,alt:i,src:c,name:s,email:l,...d},p)=>{const[v,u]=(0,g.useState)(!1),[f,k]=(0,g.useState)(!1),C=R=>{u(!0),k(!1),d.onLoad?.(R)},S=R=>{u(!1),k(!0),d.onError?.(R)},O=we(s||l),A=!!O,y=n||(!v||f)&&!A,I=!y&&!!c&&!f||void 0,B=(y&&La||void 0)??(e||void 0)??Ae(String(l||s||void 0),za,Ra)??Ra,U={src:c,alt:i,isLoaded:v,hasError:f,onLoad:C,onError:S};return(0,g.useEffect)(()=>{y&&u(!1)},[y]),(0,o.jsx)("span",{...d,ref:p,"arc-avatar":"","data-is-inactive":!t||void 0,"data-size":r,"aria-label":(d.title??s??l)||void 0,children:a||(y?(0,o.jsx)(Le,{icon:Pe,color:La}):(0,o.jsxs)(o.Fragment,{children:[I&&(0,o.jsx)(Te,{...U}),A&&(0,o.jsx)($e,{initial:O,color:B})]}))})});x("ArcAvatar",je);const Me=m`
@layer arc-components {

[arc-avatar-text] {
  display: inline-grid;
  grid-template-columns: auto 1fr;
  align-items: center;
  font-size: var(--arc-internal-avatar-text-font-size, inherit);
  gap: 0.5em;
  line-height: normal;

  &:has([data-size=xs]) {
    --arc-internal-avatar-text-font-size: var(--arc-font-size-12);
  }
  &:has([data-size=sm]) {
    --arc-internal-avatar-text-font-size: var(--arc-font-size-14);
  }
  &:has([data-size=md]) { /* @default size */
    --arc-internal-avatar-text-font-size: var(--arc-font-size-16);
  }
  &:has([data-size=lg]) {
    --arc-internal-avatar-text-font-size: var(--arc-font-size-16);
  }

  [data-element=text] {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    font-size: var(--arc-avatar-text-font-size, inherit);
  }
}

}
`,Pa="Anonymous",Oe=({children:a,avatar:r,size:e,...t})=>{const n=r.name||r.email||r.src,i=r.name||r.email||Pa,c=r.isAnonymous||!n,s=a||c?Pa:i;return(0,o.jsxs)("span",{...t,"arc-avatar-text":"",children:[(0,o.jsx)(Dr,{...r,isAnonymous:c,size:e}),(0,o.jsx)("span",{"data-element":"text",children:s})]})};x("ArcAvatarText",Me);const Be=m`
@layer arc-components {

[arc-avatar-pile] {
  position: relative;
  display: inline-flex;
  align-items: center;
  gap: 0;


  /* SIZES ---------------------------------------------------------• */

  ${Fr}

  [arc-avatar] {
    ${Ca}
  }


  /* OVERLAP MASK --------------------------------------------------• */

  /* --arc-modifier comes from avatarSizesShared */

  & {
    --arc-modifier-em: var(--arc-modifier) * 1em;
    --arc-computed-avatar-offset: calc(2 / var(--arc-modifier-em));
    --arc-computed-avatar-mask-shift: calc(-2 / var(--arc-modifier-em));

    --arc-internal-avatar-mask-position: 50% 50% at calc(-50% + var(--arc-computed-avatar-offset)) 50%;
    --arc-internal-avatar-mask-sharpness: min(var(--arc-computed-avatar-offset), 0.015em);
    --arc-internal-avatar-mask-hide: transparent calc(100% + var(--arc-computed-avatar-offset));
    --arc-internal-avatar-mask-show: black calc(100% + var(--arc-internal-avatar-mask-sharpness) + var(--arc-computed-avatar-offset));

    --arc-internal-avatar-mask: radial-gradient(
      var(--arc-internal-avatar-mask-position),
      var(--arc-internal-avatar-mask-hide),
      var(--arc-internal-avatar-mask-show)
    ) padding-box;
  }

  
  /* Cutout (mask) a portion of subsequent avatars -----------------• */
  
  [arc-avatar]+[arc-avatar] {
    margin-left: var(--arc-computed-avatar-mask-shift);
    mask: var(--arc-internal-avatar-mask); /* magic! */
  }

}

}
`,He=({size:a="md",...r})=>(0,o.jsx)("div",{...r,"arc-avatar-pile":"","data-size":a});x("ArcAvatarPile",Be);const dr=a=>typeof a=="string"?a:typeof a=="number"?a.toString():Array.isArray(a)?a.map(dr).join(""):(0,g.isValidElement)(a)?dr(a.props.children):"",Ve=()=>Math.random().toString(16).slice(-6),N=a=>`${a||"id"}-${Ve()}`,Ne=m`
  &:where([data-layout=hug]) {
    --arc-button-display: inline-flex;
    --arc-button-width: auto;
    --arc-button-content-width: fit-content;
  }

  &:where([data-layout=fill]) {
    --arc-button-display: flex;
    --arc-button-width: 100%;
    --arc-button-content-width: 0;
  }

  &:where([data-layout=icon]) {
    --arc-button-aspect-ratio: 1 / 1;
    --arc-button-flex: none;
    --arc-button-min-width: min-content;
    --arc-button-max-width: none;

    &:where([data-shape=narrow]) {
      --arc-button-aspect-ratio: auto;
    }
  }
`,Ue=m`

  &[data-size=xxl] {
    --arc-modifier: 16; /* unitless, desired font size at 100% */
    --arc-internal-btn-icon-size: 20;
    --arc-internal-btn-height: 56;
    --arc-internal-btn-gap: 10;
    --arc-internal-btn-padding-inline: 32;
    --arc-internal-btn-spinner-size: 32;
  }

  &[data-size=xl] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-btn-icon-size: 16;
    --arc-internal-btn-height: 48;
    --arc-internal-btn-gap: 8;
    --arc-internal-btn-padding-inline: 32;
    --arc-internal-btn-spinner-size: 16;
  }

  &[data-size=lg] { /** @default */
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-btn-icon-size: 16;
    --arc-internal-btn-height: 40;
    --arc-internal-btn-gap: 8;
    --arc-internal-btn-padding-inline: 20;
    --arc-internal-btn-spinner-size: 16;
  }

  &[data-size=md] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-btn-icon-size: 14;
    --arc-internal-btn-height: 32;
    --arc-internal-btn-gap: 8;
    --arc-internal-btn-padding-inline: 16;
    --arc-internal-btn-spinner-size: 16;
  }

  &[data-size=sm] {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-btn-icon-size: 12;
    --arc-internal-btn-height: 24;
    --arc-internal-btn-gap: 4;
    --arc-internal-btn-padding-inline: 12;
    --arc-internal-btn-spinner-size: 14;
  }

`,Fe=m`

&[data-variant=primary] { /** @default */
  --arc-internal-btn-text: var(--arc-color-mono-white);
  --arc-internal-btn-fill: var(--arc-color-mono-black);
  --arc-internal-btn-edge: transparent;

  --arc-internal-btn-hover-fill: var(--arc-color-neutral-800);

  --arc-internal-btn-active-text: var(--arc-color-neutral-200);
  --arc-internal-btn-active-fill: var(--arc-color-neutral-600);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);
  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-black-05);

  --arc-internal-btn-processing-fill: var(--arc-color-neutral-800);
}

&[data-variant=secondary] { /** @default - when icon-only */
  --arc-internal-btn-text: var(--arc-color-mono-black);
  --arc-internal-btn-fill: var(--arc-color-mono-white);
  --arc-internal-btn-edge: var(--arc-color-border-full);

  --arc-internal-btn-hover-fill: var(--arc-color-neutral-100);

  --arc-internal-btn-active-text: var(--arc-color-alpha-black-60);
  --arc-internal-btn-active-fill: var(--arc-color-neutral-200);
  --arc-internal-btn-active-edge: var(--arc-color-border-stark);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);
  --arc-internal-btn-disabled-edge: var(--arc-color-border-standard);
  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-white-60);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-edge: var(--arc-color-border-stark);
  --arc-internal-btn-processing-fill: var(--arc-color-alpha-white-30);

  &[data-layout=icon] {
    --arc-internal-btn-edge: var(--arc-color-border-standard);
    --arc-internal-btn-hover-edge: var(--arc-color-border-stark);
    --arc-internal-btn-focus-edge: var(--arc-color-border-standard);
  }
}

&[data-variant=tertiary] {
  --arc-internal-btn-text: var(--arc-color-mono-black);
  --arc-internal-btn-fill: transparent;
  --arc-internal-btn-edge: transparent;

  --arc-internal-btn-hover-fill: var(--arc-color-alpha-black-05);

  --arc-internal-btn-focus-fill: var(--arc-color-alpha-white-05);

  --arc-internal-btn-active-text: var(--arc-color-alpha-black-60);
  --arc-internal-btn-active-fill: var(--arc-color-alpha-black-10);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-fill: var(--arc-color-alpha-black-03);
}

`,De=m`

&[data-variant=primary-inverse] {
  --arc-internal-btn-text: var(--arc-color-mono-black);
  --arc-internal-btn-fill: var(--arc-color-mono-white);
  --arc-internal-btn-edge: transparent;

  --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-80);

  --arc-internal-btn-active-text: var(--arc-color-alpha-black-80);
  --arc-internal-btn-active-fill: var(--arc-color-alpha-white-60);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-white-40);
  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-white-20);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-fill: var(--arc-color-alpha-white-30);

  &[data-layout=icon] {
    --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-70);
  }
}

&[data-variant=secondary-inverse] {
  --arc-internal-btn-text: var(--arc-color-alpha-white-95);
  --arc-internal-btn-fill: transparent;
  --arc-internal-btn-edge: var(--arc-color-alpha-white-60);

  --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-20);
  --arc-internal-btn-hover-edge: var(--arc-color-alpha-white-80);

  --arc-internal-btn-focus-fill: var(--arc-color-alpha-black-05);
  --arc-internal-btn-focus-edge: var(--arc-color-alpha-white-60);

  --arc-internal-btn-active-text: var(--arc-color-alpha-white-95);
  --arc-internal-btn-active-fill: var(--arc-color-alpha-white-40);
  --arc-internal-btn-active-edge: var(--arc-color-alpha-white-50);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-white-40);
  --arc-internal-btn-disabled-edge: var(--arc-color-alpha-white-20);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-fill: transparent;
  --arc-internal-btn-processing-edge: var(--arc-color-alpha-white-60);

  &[data-layout=icon] {
    --arc-internal-btn-text: var(--arc-color-alpha-white-80);

    --arc-internal-btn-hover-text: var(--arc-color-alpha-white-100);
    --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-30);

    --arc-internal-btn-focus-text: var(--arc-color-alpha-white-100);
    --arc-internal-btn-focus-fill: var(--arc-color-alpha-black-05);
    --arc-internal-btn-focus-edge: var(--arc-color-alpha-white-60);

    --arc-internal-btn-active-text: var(--arc-color-alpha-white-80);
    --arc-internal-btn-active-fill: var(--arc-color-alpha-white-30);
    --arc-internal-btn-active-edge: var(--arc-color-alpha-white-40);
  }
}

&[data-variant=tertiary-inverse] {
  --arc-internal-btn-text: var(--arc-color-alpha-white-95);
  --arc-internal-btn-fill: transparent;
  --arc-internal-btn-edge: transparent;

  --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-20);

  --arc-internal-btn-active-text: var(--arc-color-alpha-white-80);
  --arc-internal-btn-active-fill: var(--arc-color-alpha-white-40);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-white-40);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-fill: var(--arc-color-alpha-white-20);

  &[data-layout=icon] {
    --arc-internal-btn-text: var(--arc-color-alpha-white-80);

    --arc-internal-btn-hover-text: var(--arc-color-alpha-white-100);
    --arc-internal-btn-hover-fill: var(--arc-color-alpha-white-30);

    --arc-internal-btn-focus-text: var(--arc-color-alpha-white-100);
    --arc-internal-btn-focus-fill: var(--arc-color-alpha-black-05);

    --arc-internal-btn-active-text: var(--arc-color-alpha-white-80);
    --arc-internal-btn-active-fill: var(--arc-color-alpha-white-30);
  }
}

`,Ye=m`

&[data-variant=ai-primary] {
  --arc-internal-btn-text: var(--arc-color-mono-white);
  --arc-internal-btn-fill: var(--arc-gradient-ai-500) padding-box, var(--arc-gradient-ai-200) border-box;

  --arc-internal-btn-hover-fill: var(--arc-gradient-ai-400) padding-box, var(--arc-gradient-ai-200) border-box;

  --arc-internal-btn-active-text: var(--arc-color-alpha-white-80);
  --arc-internal-btn-active-fill: var(--arc-gradient-ai-600) padding-box, var(--arc-gradient-ai-200) border-box;

  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-black-05);

  --arc-internal-btn-processing-text: var(--arc-color-alpha-white-70);
  --arc-internal-btn-processing-fill: var(--arc-gradient-ai-400) padding-box, var(--arc-gradient-ai-100) border-box;
}

&[data-variant=ai-secondary] {
  --arc-internal-btn-text: var(--arc-color-mono-black);
  --arc-internal-btn-fill: linear-gradient(var(--arc-color-mono-white) 0 0) padding-box, var(--arc-gradient-ai-200) border-box;

  --arc-internal-btn-hover-fill: var(--arc-gradient-ai-050) padding-box, var(--arc-gradient-ai-300) border-box;

  --arc-internal-btn-active-text: var(--arc-color-alpha-black-60);
  --arc-internal-btn-active-fill: var(--arc-gradient-ai-050) padding-box, var(--arc-gradient-ai-100) border-box;

  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-white-30);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-fill: linear-gradient(var(--arc-color-mono-white) 0 0) padding-box, var(--arc-gradient-ai-100) border-box;
}

&[data-variant^=ai-] {
  --arc-internal-btn-edge: transparent;
  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);
  --arc-internal-btn-disabled-edge: var(--arc-color-border-standard);
}

`,Ze=m`

&[data-variant=critical-primary] {
  --arc-internal-btn-text: var(--arc-color-mono-white);
  --arc-internal-btn-fill: var(--arc-color-critical-dark);
  --arc-internal-btn-edge: transparent;

  --arc-internal-btn-hover-fill: var(--arc-color-critical-heavy);

  --arc-internal-btn-active-text: var(--arc-color-critical-medium);
  --arc-internal-btn-active-fill: var(--arc-color-critical-heavy);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);
  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-black-05);

  --arc-internal-btn-processing-fill: var(--arc-color-critical-heavy);
}

&[data-variant=critical-secondary] {
  --arc-internal-btn-text: var(--arc-color-critical-dark);
  --arc-internal-btn-fill: transparent;
  --arc-internal-btn-edge: var(--arc-color-critical-dark);

  --arc-internal-btn-hover-fill: var(--arc-color-critical-light);
  --arc-internal-btn-focus-fill: var(--arc-color-alpha-white-100);

  --arc-internal-btn-active-text: var(--arc-color-red-300);
  --arc-internal-btn-active-fill: var(--arc-color-red-100);
  --arc-internal-btn-active-edge: var(--arc-color-critical-medium);

  --arc-internal-btn-disabled-text: var(--arc-color-alpha-black-30);
  --arc-internal-btn-disabled-fill: var(--arc-color-alpha-white-30);
  --arc-internal-btn-disabled-edge: var(--arc-color-border-standard);

  --arc-internal-btn-processing-text: var(--arc-color-neutral-200);
  --arc-internal-btn-processing-edge: var(--arc-color-critical-medium);
}

`,Xe=m`
  ${Fe}
  ${De}
  ${Ze}
  ${Ye}
`,qe=m`
:where([arc-button=custom]) {
  --arc-button-font-weight: normal;
  --arc-button-justify-content: flex-start;
  --arc-button-disabled-opacity: var(--arc-alpha-30);

  /* Default size matches [data-size=lg] */
  --arc-modifier: 14; /* unitless, desired font size at 100% */
  --arc-modifier-unit: 0.0625em; /* 1/16th of an em for the computed calcs */
  --arc-internal-btn-icon-size: 16;
  --arc-internal-btn-gap: 8;
  --arc-internal-btn-padding-inline: 0;
  --arc-internal-btn-spinner-size: 16;
}
`,We=m`
@layer arc-components {

[arc-button] {
  ${Ue}
  ${Ne}
  ${Xe}

  --arc-button-color: var(--arc-internal-btn-text);
  --arc-button-background: var(--arc-internal-btn-fill);
  --arc-internal-btn-edge-color: var(--arc-internal-btn-edge);

  &[data-layout=icon] {
    --arc-internal-btn-padding-inline: 0;

    &[data-shape=narrow] {
      --arc-internal-btn-padding-inline: 2;
    }
  }

  --arc-modifier-base: var(--arc-modifier) * var(--arc-modifier-unit, 1px);
  --arc-modifier-em: var(--arc-modifier) * 1em;

  /* COMPUTED VARIABLES ------------------------------------------• */

  --arc-computed-btn-font-size: calc(var(--arc-modifier-base));
  --arc-computed-btn-icon-size: calc(var(--arc-internal-btn-icon-size) / var(--arc-modifier-em));
  --arc-computed-btn-border-size: max(var(--arc-border-width-sm), calc(1 / var(--arc-modifier-em)));
  --arc-computed-btn-outline-size: max(var(--arc-border-width-md), calc(2 / var(--arc-modifier-em)));
  --arc-computed-btn-border-radius-md: max(var(--arc-border-radius-md), calc(4 / var(--arc-modifier-em)));
  --arc-computed-btn-height: calc(var(--arc-internal-btn-height) / var(--arc-modifier-em));
  --arc-computed-btn-padding: 0 calc(var(--arc-internal-btn-padding-inline) / var(--arc-modifier-em));
  --arc-computed-btn-gap: calc(var(--arc-internal-btn-gap) / var(--arc-modifier-em));
  --arc-computed-btn-spinner-size: calc(var(--arc-internal-btn-spinner-size) / var(--arc-modifier-em));

  --arc-computed-btn-border: var(--arc-computed-btn-border-size) solid var(--arc-internal-btn-edge-color, transparent);
  --arc-computed-btn-outline: var(--arc-computed-btn-outline-size) solid var(--arc-internal-btn-outline-color, var(--arc-color-border-focus));

  &[data-variant^=ai-] {
    --arc-computed-btn-border-size: max(var(--arc-border-width-md), calc(2 / var(--arc-modifier-em)));
  }

  &:is(:not([data-shape]), [data-shape=circle]) {
    --arc-computed-btn-border-radius: var(--arc-border-radius-pill); /** @computed • border-radius */
  }

  &:is(
    [arc-button=custom],
    [data-layout=icon]:not([data-shape]),
    [data-shape=square],
    [data-shape=narrow]
  ) {
    --arc-computed-btn-border-radius: var(--arc-computed-btn-border-radius-md); /** @computed • border-radius */
  }

  /* Inter-Component Communication ---------------------------------• */
  --arc-icon-fa-line-height: 1lh;
  --arc-icon-size: var(--arc-computed-btn-icon-size); /** ArcIcon, @computed • icon-size */

  /* STATES --------------------------------------------------------- */

  &:is([is-processing], :has(input)) {
    --arc-button-position: relative;
  }

  &:is([is-disabled]) {
    --arc-button-cursor: not-allowed;
    --arc-button-opacity: var(--arc-button-disabled-opacity, unset);
    --arc-button-color: var(--arc-internal-btn-disabled-text, var(--arc-internal-btn-text));
    --arc-button-background: var(--arc-internal-btn-disabled-fill, var(--arc-internal-btn-fill));
    --arc-internal-btn-edge-color: var(--arc-internal-btn-disabled-edge, var(--arc-internal-btn-edge));
  }

  &:not([is-disabled], [is-processing]) {
    &:hover {
      --arc-button-color: var(--arc-internal-btn-hover-text, var(--arc-internal-btn-text));
      --arc-button-background: var(--arc-internal-btn-hover-fill, var(--arc-internal-btn-fill));
      --arc-internal-btn-edge-color: var(--arc-internal-btn-hover-edge, var(--arc-internal-btn-edge));
    }

    &:is(:active, [is-active]) {
      --arc-button-color: var(--arc-internal-btn-active-text, var(--arc-internal-btn-text));
      --arc-button-background: var(--arc-internal-btn-active-fill, var(--arc-internal-btn-fill));
      --arc-internal-btn-edge-color: var(--arc-internal-btn-active-edge, var(--arc-internal-btn-edge));
    }
  }

  &[is-processing] {
    --arc-button-cursor: default;
    --arc-button-content-visibility: hidden;
    --arc-button-content-opacity: 0;
    --arc-button-opacity: var(--arc-button-processing-opacity, unset);

    --arc-button-color: var(--arc-internal-btn-processing-text, var(--arc-internal-btn-text));
    --arc-button-background: var(--arc-internal-btn-processing-fill, var(--arc-internal-btn-fill));
    --arc-internal-btn-edge-color: var(--arc-internal-btn-processing-edge, var(--arc-internal-btn-edge));

    /* spinner svg style controls */
    --arc-button-spinner-opacity: var(--arc-alpha-30);
    --arc-button-spinner-circle-color: var(--arc-button-color);
    --arc-button-spinner-progress-color: var(--arc-button-color);

    &:is(
      [data-variant*=secondary], /* contains 'secondary' */
      [data-variant*=tertiary], /* contains 'tertiary' */
      [data-variant^=ai-], /* starts with 'ai-' */
      [data-variant$=-inverse] /* ends with '-inverse' */
    ) {
      --arc-button-spinner-opacity: var(--arc-alpha-100);
      --arc-button-spinner-progress-color: var(--arc-color-brand-root);
    }
  }

  &:where(:focus, :focus-within):focus-visible {
    --arc-button-color: var(--arc-internal-btn-focus-text, var(--arc-internal-btn-text));
    --arc-button-background: var(--arc-internal-btn-focus-fill, var(--arc-internal-btn-fill));
    --arc-internal-btn-edge-color: var(--arc-internal-btn-focus-edge, var(--arc-internal-btn-edge));
    --arc-internal-btn-outline-color: var(--arc-color-border-focus);


    &[data-variant$=-inverse] {
      --arc-internal-btn-outline-color: var(--arc-color-border-focus-inverse);
    }

    outline: var(--arc-button-outline, var(--arc-computed-btn-outline));
    outline-offset: var(--arc-button-outline-offset, var(--arc-computed-btn-outline-size)); /** @computed • outline-offset */

    * {
      outline: none;
    }
  }
}

/* CORE STYLES ------------------------------------------------------ */

[arc-button] {
  appearance: none;
  cursor: var(--arc-button-cursor, pointer);
  position: var(--arc-button-position, unset);
  transition: 150ms ease-out;

  /* footprint */

  font: inherit;
  font-size: var(--arc-button-font-size, var(--arc-computed-btn-font-size, inherit)); /** @computed • font-size */
  font-weight: var(--arc-button-font-weight, var(--arc-font-weight-600));
  max-width: var(--arc-button-max-width, 100%);
  min-width: var(--arc-button-min-width, unset);
  width: var(--arc-button-width, unset);
  height: var(--arc-button-height, var(--arc-computed-btn-height, unset)); /** @computed • height */
  aspect-ratio: var(--arc-button-aspect-ratio, unset);

  padding: var(--arc-button-padding, var(--arc-computed-btn-padding)); /** @computed • padding */

  /* Variant Theme Applied -----------------------------------------• */

  color: var(--arc-button-color, unset);
  background: var(--arc-button-background, transparent);
  border: var(--arc-button-border, var(--arc-computed-btn-border)); /** @computed • border */
  border-radius: var(--arc-button-border-radius, var(--arc-computed-btn-border-radius)); /** @computed • border-radius */
  opacity: var(--arc-button-opacity, 1);

  &[href] {
    text-decoration: var(--arc-button-text-decoration, none);
  }

  /* INNER-ELEMENTS ------------------------------------------------• */

  [arc-button-content] {
    flex: 1;
    width: var(--arc-button-content-width, auto);
    display: flex;
    align-items: center;
    justify-content: var(--arc-button-justify-content, center);
    gap: var(--arc-button-gap, max(var(--arc-space-0-5), var(--arc-computed-btn-gap, 0.5em))); /** @computed • gap */
    max-width: 100%;
    overflow: hidden;
    pointer-events: var(--arc-button-content-pointer-events, none);
    user-select: none;
    visibility: var(--arc-button-content-visibility, unset);
    opacity: var(--arc-button-content-opacity, unset);
    line-height: normal;
  }

  [arc-button-label] {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    max-width: 100%;
    transition: 0.2s ease-out;
  }

  [arc-button-icon] {
    transition: 0.2s ease-out;
  }

  :is([arc-spinner], input) {
    position: absolute;
    inset: var(--arc-spinner-inset, 50% auto auto 50%);
    translate: var(--arc-spinner-translate, -50% -50%);
  }

  input {
    opacity: 0;
    scale: 0.1;
  }

  [arc-spinner] {
    display: flex;
    pointer-events: none;
    border-radius: 50%;
    width: var(--arc-spinner-size, var(--arc-computed-btn-spinner-size)); /** @computed • spinner size */
    aspect-ratio: 1 / 1;

    [arc-spinner-circle] {
      fill: none;
      stroke: var(--arc-button-spinner-circle-color, currentcolor);
      opacity: var(--arc-button-spinner-opacity, var(--arc-alpha-40));
    }

    [arc-spinner-progress] {
      fill: none;
      stroke: var(--arc-button-spinner-progress-color, currentcolor);
      stroke-dasharray: 0.3334 1;
      stroke-dashoffset: 0;
      stroke-linecap: round;
      animation: 743ms arc-spinner-rotate linear infinite;
      transform-origin: 50% 50%;
    }
  }
}

${qe}

[arc-button]:not([hidden]) {
  flex: var(--arc-button-flex, unset);
  display: var(--arc-button-display, inline-flex);
  align-items: center;
  justify-content: center;
}

@keyframes arc-spinner-rotate {
  to {
    transform: rotate3d(0, 0, 1, 360deg);
  }
}

}
`,Ke=h($r,["circle","square","narrow"]),Ge=h(_,["xxl","xl","lg","md","sm"]),Je=h(P,["hug","fill","icon"]),Qe=h($,["primary","secondary","tertiary","critical-primary","critical-secondary","ai-primary","ai-secondary","primary-inverse","secondary-inverse","tertiary-inverse"]),rt=(0,o.jsxs)("svg",{viewBox:"0 0 100 100","arc-spinner":"",children:[(0,o.jsx)("circle",{"arc-spinner-circle":"",cx:"50%",cy:"50%",r:"45%",strokeWidth:"12.5%",pathLength:"1"}),(0,o.jsx)("circle",{"arc-spinner-progress":"",cx:"50%",cy:"50%",r:"45%",strokeWidth:"12.5%",pathLength:"1"})]}),W=(0,g.forwardRef)(({children:a,...r},e)=>{const{iconEnd:t,iconStart:n,icon:i,text:c,isActive:s,isDisabled:l,isProcessing:d,tag:p,href:v,type:u,title:f,layout:k,shape:C,size:S="lg",variant:O="primary",_isCustom:A,...y}=r,I=k==="icon",B=p??(v?"a":"button"),U=B==="button"?u||"button":void 0,R=d||l||r.disabled,Ar=n||i,zr=!I&&(a||c),Cr=!I&&t;return(0,o.jsxs)(B,{...y,ref:e,"arc-button":A?"custom":"",href:v||void 0,title:f||void 0,type:U,"data-layout":k,"data-shape":A?void 0:C,"data-size":A?void 0:S,"data-variant":A?void 0:O,"is-active":s?"":void 0,"is-processing":d?"":void 0,"is-disabled":R?"":void 0,"aria-disabled":R||void 0,children:[(0,o.jsxs)("span",{"arc-button-content":"",children:[!!Ar&&(0,o.jsx)(w,{"arc-button-icon":"",icon:Ar}),!!zr&&(0,o.jsx)("span",{"arc-button-label":"",children:zr}),!!Cr&&(0,o.jsx)(w,{"arc-button-icon":"",icon:t})]}),d&&rt]})});W.displayName="ArcButton",x("ArcButton",We);const at=(a,r)=>{if(!a||!r)return;const e=a.getBoundingClientRect(),t=document.getElementById(r);t&&(t.style.top=`${e.bottom+window.scrollY}px`,t.style.left=`${e.right+window.scrollX}px`)},et=({children:a,popovertarget:r,popovertargetaction:e,...t})=>{const n=(0,g.useRef)(null),i=()=>at(n.current,r);return(0,g.useEffect)(()=>(window.addEventListener("scroll",i),window.addEventListener("resize",i),()=>{window.removeEventListener("scroll",i),window.removeEventListener("resize",i)}),[r]),(0,o.jsx)(W,{...t,ref:n,"aria-haspopup":"true",popovertarget:r,popovertargetaction:e||void 0,onClick:c=>{c.stopPropagation(),i(),t.onClick?.(c)},children:a})},tt=m`
@layer arc-components {

[arc-_popover] {
  translate: var(--arc-popover-translate, -50% -50%);
  inset: var(--arc-popover-inset, auto);
  position: var(--arc-popover-position, absolute);

  font-size: var(--arc-popover-font-size, var(--arc-font-size-14));

  width: var(--arc-popover-width, auto);
  height: var(--arc-popover-height, auto);
  max-width: var(--arc-popover-max-width, 65vw);
  max-height: var(--arc-popover-max-height, 35vh);
  border-radius: var(--arc-popover-border-radius, var(--arc-border-radius-lg));
  box-shadow: var(--arc-popover-box-shadow, var(--arc-shadow-xl));

  border: var(--arc-popover-border, none);
  margin: var(--arc-popover-margin, auto);
  padding: var(--arc-popover-padding, 0);

  color: var(--arc-popover-color, var(--arc-color-mono-white));
  background: var(--arc-popover-background, var(--arc-color-alpha-black-90));

  overscroll-behavior: contain;
}

}
`,nt=({popover:a,tag:r="div",...e})=>(0,o.jsx)(r,{...e,"arc-_popover":"",popover:a||""});x("_Popover",tt);const ot=m`
@layer arc-components {

[arc-avatar-overflow] {

  /* Inter-Component Communication • ArcPopover --------------------• */

  --arc-popover-translate: -100% 0.5em;
  --arc-popover-width: max-content;
  --arc-popover-max-width: 18em;

  /* Neutral/Anonymous avatars -------------------------------------• */

  --arc-internal-neutral-color: var(--arc-color-alpha-white-70);
  --arc-internal-neutral-background: var(--arc-color-alpha-white-20);

  [data-element="list"] {
    margin: 0;
    padding: var(--arc-space-0-5) var(--arc-space-0);
  }

  [data-element="list-item"] {
    display: flex;
    align-items: center;
    gap: var(--arc-space-0-75);
    padding: var(--arc-space-0-5) var(--arc-space-1);
  }

  /* Sizes ---------------------------------------------------------• */
  ${Fr}

  [arc-avatar] {
    ${Ca}
  }
}

}
`,it=a=>(0,o.jsx)("li",{...a,"data-element":"list-item"}),ct=({children:a,id:r,size:e="sm",overflowCount:t=0,shouldShowOverflow:n=!1,...i})=>{const c=Math.min(t,999),s=(0,g.useMemo)(()=>r||N("arc-avatar-overflow"),[r]),l=g.Children.toArray(a);return(0,o.jsxs)(o.Fragment,{children:[c>0&&(0,o.jsx)(Dr,{title:`+${c}`,children:(0,o.jsx)(et,{popovertarget:s,disabled:!n,"data-element":"counter","data-color":"black","data-num-length":c?.toString().length,children:`+${c}`})}),n&&(0,o.jsx)(nt,{...i,id:s,"arc-avatar-overflow":"","data-size":e,children:(0,o.jsx)("ul",{"data-element":"list",children:l.map((d,p)=>(0,o.jsx)(it,{children:d},p))})})]})};x("ArcAvatarOverflow",ot);const lt=m`
@layer arc-components {

[arc-icon-text] {

  /* VARIANTS --------------------------------------------------------• */

  &[data-variant=inherit] { /* @default 'inherit' • uses font/color props from context */
    --arc-internal-icontext-text-color: currentcolor;
    --arc-internal-icontext-icon-color: currentcolor;
  }

  &[data-variant=primary] {
    --arc-internal-icontext-text-color: var(--arc-color-text-primary);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-primary);
  }

  &[data-variant=secondary] {
    --arc-internal-icontext-text-color: var(--arc-color-text-secondary);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-secondary);
  }

  &[data-variant=inverse] {
    --arc-internal-icontext-text-color: var(--arc-color-text-inverse);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-inverse);
  }

  &[data-variant=brand] {
    --arc-internal-icontext-text-color: var(--arc-color-text-brand);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-brand);
  }

  &[data-variant=info] {
    --arc-internal-icontext-text-color: var(--arc-color-text-info);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-info);
  }

  &[data-variant=success] {
    --arc-internal-icontext-text-color: var(--arc-color-text-success);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-success);
  }

  &[data-variant=warning] {
    --arc-internal-icontext-text-color: var(--arc-color-text-warning);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-warning);
  }

  &[data-variant=critical] {
    --arc-internal-icontext-text-color: var(--arc-color-text-critical);
    --arc-internal-icontext-icon-color: var(--arc-color-icon-critical);
  }


  /* SIZES -----------------------------------------------------------• */

  &[data-size=inherit] { /* @default 'inherit' • uses font/size from context */
    --arc-inherit-icontext-font-size: unset;
    --arc-inherit-icontext-gap: 0.375em; /* 6px @ 100%/16px base */
  }

  &[data-size=md] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-icontext-gap: 4;
  }

  &[data-size=sm] {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-icontext-gap: 4;
  }


  /* COMPUTED ------------------------------------------------------• */
  & {
    --arc-modifier-px: var(--arc-icon-text-modifier, var(--arc-modifier) * 1px);
    --arc-modifier-em: var(--arc-icon-text-modifier, var(--arc-modifier) * 1em);

    --arc-computed-icontext-font-size: var(--arc-inherit-icontext-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-icontext-gap: max(var(--arc-space-0-5), var(--arc-inherit-icontext-gap, calc(var(--arc-internal-icontext-gap) / var(--arc-modifier-em))));

    /* Inter-Component Communication -------------------------------• */
    --arc-icon-svg-align-self: flex-start; /** ArcIcon • align-self for SVGs */
    --arc-icon-color: var(--arc-icon-text-icon-color, var(--arc-internal-icontext-icon-color)); /** ArcIcon • color */

    &[data-layout=inline] {
      --arc-icon-svg-height: 0.9lh; /* ArcIcon • adjust height of SVGs for inline layout */
    }
  }

}


/* CORE STYLES -----------------------------------------------------• */

[arc-icon-text] {
  position: var(--arc-icon-text-position, relative);
  font-size: var(--arc-icon-text-font-size, var(--arc-computed-icontext-font-size));
  color: var(--arc-icon-text-text-color, var(--arc-internal-icontext-text-color));

  &[data-layout=gutter] { /* @default */
    display: inline-flex;
    align-items: var(--arc-icon-text-align-items, first baseline);
    gap: var(--arc-icon-text-gap, var(--arc-computed-icontext-gap));

    &[data-is-flipped] {
      flex-direction: row-reverse;
    }

    /* Truncation Support */
    overflow: var(--arc-icon-text-overflow, unset);

    [data-element=text] {
      overflow: var(--arc-icon-text-overflow, unset);
      text-overflow: var(--arc-icon-text-text-overflow, unset);
      white-space: var(--arc-icon-text-white-space, unset);
    }
  }

  &[data-layout=inline] {
    display: inline;

    [data-element=icon] + [data-element=text] {
      margin-inline-start: var(--arc-icon-text-gap, var(--arc-computed-icontext-gap));
    }

    [data-element=text]:has(+ [data-element=icon]) {
      margin-inline-end: var(--arc-icon-text-gap, var(--arc-computed-icontext-gap));
    }
  }

  [data-element=text] {
    position: relative;
    hyphens: auto;
  }
}

}
`,st=h(_,["inherit","md","sm"]),dt=h(P,["gutter","inline"]),pt=h($,["inherit","primary","secondary","inverse","brand","info","success","warning","critical"]),pr=({children:a,icon:r,iconSize:e,text:t,size:n="inherit",tag:i="span",variant:c="inherit",layout:s="gutter",isFlipped:l=!1,...d})=>{const p=a||t,v=!!r&&(!l||s==="gutter"),u=!!r&&!!p&&l&&s==="inline";return(0,o.jsxs)(i,{...d,"arc-icon-text":"","data-is-flipped":l?"":void 0,"data-variant":c,"data-layout":s,"data-size":n,children:[v&&(0,o.jsx)(w,{"data-element":"icon",icon:r,size:e}),p&&(0,o.jsx)("span",{"data-element":"text",children:p}),u&&(0,o.jsx)(w,{"data-element":"icon",icon:r,size:e})]})};x("ArcIconText",lt);const vt=m`
@layer arc-components {

[arc-badge-wrapper] {

  /* VARIANTS ------------------------------------------------------• */

  &:has(>[data-variant=brand-black]) {
    --arc-internal-badge-background-high: var(--arc-color-mono-black);
    --arc-internal-badge-background-low: var(--arc-color-neutral-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-text-primary);
  }

  /** @default */
  &:has(>[data-variant=brand-blue]) {
    --arc-internal-badge-background-high: var(--arc-color-blue-500);
    --arc-internal-badge-background-low: var(--arc-color-blue-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-blue-700);
  }

  &:has(>[data-variant=neutral]) {
    --arc-internal-badge-background-high: var(--arc-color-neutral-600);
    --arc-internal-badge-background-low: var(--arc-color-neutral-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-neutral-800);
  }

  &:has(>[data-variant=info]) {
    --arc-internal-badge-background-high: var(--arc-color-info-dark);
    --arc-internal-badge-background-low: var(--arc-color-purple-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-info-heavy);
  }

  &:has(>[data-variant=success]) {
    --arc-internal-badge-background-high: var(--arc-color-success-dark);
    --arc-internal-badge-background-low: var(--arc-color-green-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-success-heavy);
  }

  &:has(>[data-variant=warning]) {
    --arc-internal-badge-background-high: var(--arc-color-warning-root);
    --arc-internal-badge-background-low: var(--arc-color-yellow-100);
    --arc-internal-badge-color-high: var(--arc-color-text-primary);
    --arc-internal-badge-color-low: var(--arc-color-warning-heavy);
  }

  &:has(>[data-variant=critical]) {
    --arc-internal-badge-background-high: var(--arc-color-critical-root);
    --arc-internal-badge-background-low: var(--arc-color-red-100);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-critical-heavy);
  }

  &:has(>[data-variant=ai]) {
    --arc-internal-badge-background-high: var(--arc-gradient-ai-200);
    --arc-internal-badge-background-low: var(--arc-gradient-ai-200);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-text-inverse);
    --arc-internal-badge-mix-blend-mode-low: normal;
    --arc-internal-badge-mix-blend-mode-high: normal;
  }

  &:has(>[data-variant=overlay-light]) {
    --arc-internal-badge-background-high: var(--arc-color-alpha-white-80);
    --arc-internal-badge-background-low: var(--arc-color-alpha-white-20);
    --arc-internal-badge-color-high: var(--arc-color-text-primary);
    --arc-internal-badge-color-low: var(--arc-color-text-inverse);
    --arc-internal-badge-mix-blend-mode-low: normal;
  }

  &:has(>[data-variant=overlay-dark]) {
    --arc-internal-badge-background-high: var(--arc-color-alpha-black-70);
    --arc-internal-badge-background-low: var(--arc-color-alpha-black-40);
    --arc-internal-badge-color-high: var(--arc-color-text-inverse);
    --arc-internal-badge-color-low: var(--arc-color-text-inverse);
    --arc-internal-badge-mix-blend-mode-low: normal;
  }


  /* CONTRASTS -----------------------------------------------------• */

  &:has(>[data-contrast=high]) {
    --arc-internal-badge-color: var(--arc-internal-badge-color-high);
    --arc-internal-badge-background: var(--arc-internal-badge-background-high);
    --arc-internal-badge-mix-blend-mode: var(--arc-internal-badge-mix-blend-mode-high, normal);
  }

  &:has(>[data-contrast=low]) {
    --arc-internal-badge-color: var(--arc-internal-badge-color-low);
    --arc-internal-badge-background: var(--arc-internal-badge-background-low);
    --arc-internal-badge-mix-blend-mode: var(--arc-internal-badge-mix-blend-mode-low, multiply);
  }


  /* SIZES ---------------------------------------------------------• */

  &:has(>[data-size=lg]) {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-badge-font-size-uppercase: 12;
    --arc-internal-badge-size: 24;
    --arc-internal-badge-min-size: 30;
    --arc-internal-badge-padding-inline: 8;
    --arc-internal-badge-border-radius: 4;
  }

  &:has(>[data-size=md], >[data-size=inherit]) {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-badge-font-size-uppercase: 10;
    --arc-internal-badge-size: 20;
    --arc-internal-badge-min-size: 24;
    --arc-internal-badge-padding-inline: 6;
    --arc-internal-badge-border-radius: 4;
  }

  /** @default • relative equivalent of 'md' at 100% • (16 x 75% = 12) */
  &:has(>[data-size=inherit]) {
    --arc-inherit-badge-font-size: 0.75em;
    --arc-inherit-badge-min-size: 1.5em;
  }

  &:has(>[data-size=sm]) {
    --arc-modifier: 10; /* unitless, desired font size at 100% */
    --arc-internal-badge-font-size-uppercase: 8;
    --arc-internal-badge-size: 16;
    --arc-internal-badge-min-size: 18;
    --arc-internal-badge-padding-inline: 4;
    --arc-internal-badge-border-radius: 4;
  }


  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-badge-font-size: var(--arc-inherit-badge-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-badge-font-size-uppercase: calc(var(--arc-internal-badge-font-size-uppercase) / var(--arc-modifier-em));
    --arc-computed-badge-size: calc(var(--arc-internal-badge-size) / var(--arc-modifier-em));
    --arc-computed-badge-padding-inline: calc(var(--arc-internal-badge-padding-inline) / var(--arc-modifier-em));
    --arc-computed-badge-border-radius: calc(var(--arc-internal-badge-border-radius) / var(--arc-modifier-em));
    --arc-computed-badge-min-width: var(--arc-inherit-badge-min-size, calc(var(--arc-internal-badge-min-size) * 1px));

    --arc-internal-badge-min-width: min-content;

    /* Inter-Component Communication • ArcIconText -------------------• */

    --arc-icon-text-modifier: var(--arc-modifier);
    --arc-icon-text-align-items: center;
  }


  /* LAYOUTS -------------------------------------------------------• */

  &:has(>[data-layout=fill]) {
    --arc-internal-badge-flex: 1;
    --arc-internal-badge-display: flex;
    --arc-internal-badge-min-width: var(--arc-computed-badge-min-width);

    /* Inter-Component Communication • ArcIconText -------------------• */
    --arc-icon-text-overflow: hidden;
    --arc-icon-text-white-space: nowrap;
    --arc-icon-text-text-overflow: ellipsis;
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-badge-wrapper]:not([hidden]) {
  flex: var(--arc-badge-flex, var(--arc-internal-badge-flex, unset));
  align-self: var(--arc-badge-align-self, flex-start);
  display: var(--arc-internal-badge-display, inline-flex);
  align-items: center;
  justify-content: center;
  height: 1lh;
  max-height: 1lh;
  vertical-align: bottom;

  &:has(>[data-is-flexible]) {
    min-width: var(--arc-badge-min-width, var(--arc-internal-badge-min-width));
  }
}


[arc-badge] {
  font-size: var(--arc-badge-font-size, var(--arc-computed-badge-font-size));
  font-weight: var(--arc-font-weight-600);

  flex: 1;
  display: var(--arc-internal-badge-display, inline-flex);
  align-items: center;
  justify-content: center;
  height: var(--arc-computed-badge-size); /** @computed • height */
  padding-inline: var(--arc-computed-badge-padding-inline); /** @computed • padding-inline */

  overflow: hidden;
  white-space: nowrap;
  line-height: 2;

  color: var(--arc-badge-color, var(--arc-internal-badge-color));
  background: var(--arc-badge-background, var(--arc-internal-badge-background));
  border-radius: var(--arc-computed-badge-border-radius); /** @computed • border-radius */
  mix-blend-mode: var(--arc-badge-mix-blend-mode, var(--arc-internal-badge-mix-blend-mode));

  &[data-is-uppercase] [data-element=text] {
    text-transform: uppercase;
    font-size: var(--arc-computed-badge-font-size-uppercase); /** @computed • font-size */
  }
}

}
`,ht=h(_,["inherit","lg","md","sm"]),ut=h(P,["hug","fill"]),bt=["high","low"],mt=h($,["brand-black","brand-blue","neutral","info","success","warning","critical","ai","overlay-light","overlay-dark"]),gt=({children:a,icon:r,text:e,isFlipped:t,isFlexible:n,isUppercase:i,variant:c="brand-blue",size:s="inherit",layout:l="hug",contrast:d="high",...p})=>{const v=a||(0,o.jsx)(pr,{icon:r,text:e,isFlipped:t,layout:"gutter"}),u=l==="fill"&&n;return(0,o.jsx)("span",{...p,"arc-badge-wrapper":"",children:(0,o.jsx)("span",{"arc-badge":"","data-contrast":d,"data-variant":c,"data-layout":l,"data-size":s,"data-is-flexible":u?"":void 0,"data-is-uppercase":i?"":void 0,children:v})})};x("ArcBadge",vt);const ft=m`
@layer arc-components {

[arc-badge-count] {

  /* VARIANTS ------------------------------------------------------• */

  &[data-variant=neutral] { /** @default */
    --arc-internal-badgecount-color: var(--arc-color-neutral-800);
    --arc-internal-badgecount-background: var(--arc-color-neutral-100);
    --arc-internal-badgecount-mix-blend-mode: multiply;
  }

  &[data-variant=brand] {
    --arc-internal-badgecount-color: var(--arc-color-blue-700);
    --arc-internal-badgecount-background: var(--arc-color-blue-100);
    --arc-internal-badgecount-mix-blend-mode: multiply;
  }

  &[data-variant=success] {
    --arc-internal-badgecount-color: var(--arc-color-success-heavy);
    --arc-internal-badgecount-background: var(--arc-color-green-100);
    --arc-internal-badgecount-mix-blend-mode: multiply;
  }

  &[data-variant=warning] {
    --arc-internal-badgecount-color: var(--arc-color-warning-heavy);
    --arc-internal-badgecount-background: var(--arc-color-yellow-100);
    --arc-internal-badgecount-mix-blend-mode: multiply;
  }

  &[data-variant=critical] {
    --arc-internal-badgecount-color: var(--arc-color-text-inverse);
    --arc-internal-badgecount-background: var(--arc-color-critical-root);
    --arc-internal-badgecount-mix-blend-mode: normal;
  }

  &[data-variant=inverse] {
    --arc-internal-badgecount-color: var(--arc-color-text-primary);
    --arc-internal-badgecount-background: var(--arc-color-mono-white);
    --arc-internal-badgecount-mix-blend-mode: normal;
  }


  /* SIZES ---------------------------------------------------------• */

  &[data-size=lg] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-badgecount-size: 24;
    --arc-internal-badgecount-padding-inline: 4;
  }

  &:is([data-size=md], [data-size=inherit]) {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-badgecount-size: 20;
    --arc-internal-badgecount-padding-inline: 4;
  }

  &[data-size=sm] {
    --arc-modifier: 10; /* unitless, desired font size at 100% */
    --arc-internal-badgecount-size: 16;
    --arc-internal-badgecount-padding-inline: 4;
  }

  &[data-size=inherit] { /** @default • scalable based on 'md' size */
    --arc-inherit-badgecount-font-size: 0.75em;
  }


  /* SHAPES ------------------------------------------------------• */

  &[data-shape=square] {
    --arc-internal-badgecount-border-radius: 4;
  }


  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-badgecount-font-size: var(--arc-inherit-badgecount-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-badgecount-size: calc(var(--arc-internal-badgecount-size) / var(--arc-modifier-em));
    --arc-computed-badgecount-padding-inline: calc(var(--arc-internal-badgecount-padding-inline) / var(--arc-modifier-em));
    --arc-computed-badgecount-border-radius: calc(var(--arc-internal-badgecount-border-radius) / var(--arc-modifier-em));
  }

  &[data-shape=circle] {
    --arc-computed-badgecount-border-radius: var(--arc-border-radius-pill);
  }
}


/* CORE STYLES ------------------------------------------------------ */

[arc-badge-count-wrapper]:not([hidden]) {
  flex: none;
  align-self: var(--arc-badge-count-align-self, flex-start);
  display: inline-flex;
  align-items: center;
  justify-content: center;
  height: 1lh;
  max-height: 1lh;
  vertical-align: bottom;
}

[arc-badge-count] {
  mix-blend-mode: var(--arc-badge-count-mix-blend-mode, var(--arc-internal-badgecount-mix-blend-mode));
  font-size: var(--arc-badge-count-font-size, var(--arc-computed-badgecount-font-size));
  font-weight: var(--arc-font-weight-600);

  flex: none;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  height: var(--arc-computed-badgecount-size); /** @computed • height */
  min-width: var(--arc-computed-badgecount-size); /** @computed • min-width */
  padding-inline: var(--arc-computed-badgecount-padding-inline); /** @computed • padding-inline */

  white-space: nowrap;
  line-height: normal;

  color: var(--arc-badge-count-color, var(--arc-internal-badgecount-color));
  background: var(--arc-badge-count-background, var(--arc-internal-badgecount-background));
  border-radius: var(--arc-computed-badgecount-border-radius); /** @computed • border-radius */
}

`,xt=h(_,["inherit","lg","md","sm"]),yt=h($,["neutral","brand","critical","warning","success","inverse"]),wt=h($r,["square","circle"]),kt=({children:a,text:r,variant:e="neutral",size:t="inherit",shape:n="square",...i})=>{const c=a||r;return(0,o.jsx)("span",{...i,"arc-badge-count-wrapper":"",children:(0,o.jsx)("span",{"arc-badge-count":"","data-variant":e,"data-shape":n,"data-size":t,children:c})})};x("ArcBadgeCount",ft);const At=m`
@layer arc-components {

[arc-label] {

  /* Inter-Component Communication ---------------------------------• */

  /* also to: ArcLabelContent */
  --arc-internal-label-opacity: var(--arc-label-opacity, unset);
  --arc-internal-label-opacity-disabled: var(--arc-label-opacity-disabled, var(--arc-alpha-60));


  /* Internal STYLES -----------------------------------------------• */

  --arc-internal-label-cursor: var(--arc-label-cursor, pointer);
  --arc-internal-label-cursor-disabled: var(--arc-label-cursor-disabled, not-allowed);

  &:has(:disabled) {
    --arc-internal-label-cursor: var(--arc-internal-label-cursor-disabled);
    --arc-internal-label-opacity: var(--arc-internal-label-opacity-disabled);
  }

  &[data-layout=fill] {
    --arc-internal-label-display: flex;
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-label]:not([hidden]) {
  display: var(--arc-internal-label-display, inline-flex);
  align-items: var(--arc-label-align-items, baseline);
  gap: var(--arc-label-gap, max(var(--arc-space-1), 0.5em));
  cursor: var(--arc-internal-label-cursor);
}

}
`,zt=h(P,["hug","fill"]),Q=({layout:a,...r})=>(0,o.jsx)("label",{...r,"arc-label":"","data-layout":a});x("ArcLabel",At);const Ct=m`
@layer arc-components {

[arc-label-content] {
  opacity: var(--arc-label-opacity, var(--arc-internal-label-opacity, unset));
  transition: var(--arc-label-transition, opacity 100ms ease-out);
  user-select: var(--arc-label-user-select, none);
  -webkit-user-select: var(--arc-label-user-select, none);
}

}
`,rr=a=>(0,o.jsx)("span",{...a,"arc-label-content":""});x("LabelContent",Ct);const St={checkbox:"checkbox",radio:"radio",switch:"radio"},wr=({id:a,indeterminate:r,type:e,onChange:t,...n})=>{const i=e==="checkbox"&&!!r,c=(0,g.useRef)(null),s=(0,g.useMemo)(()=>a||N(`arc-${e}`),[a]),l=d=>p=>{if(!(p?.target instanceof HTMLInputElement))return;const v={checked:p?.target.checked,indeterminate:p?.target?.indeterminate,name:p?.target?.name,value:n?.value};d?.(v,p)};return(0,g.useEffect)(()=>{c.current?.type==="checkbox"&&(c.current.indeterminate=!!r)},[r]),(0,o.jsx)("input",{...n,id:s,ref:c,type:St[e],"arc-input-type":e,"aria-disabled":n?.disabled||void 0,"data-is-disabled":n?.disabled||void 0,"data-is-checked":n?.checked||void 0,"data-is-indeterminate":i||void 0,onChange:l(t)})},_t=m`
@layer arc-components {

[arc-input-mask] {

  /* Inter-Component Communication ---------------------------------• */

  --arc-icon-size: var(--arc-internal-inputmask-icon-size); /* ArcIcon */


  /* --arc-internal- Tokens • NOT FOR CONSUMER USE -----------------• */

  --arc-internal-inputmask-border-width: max(var(--arc-border-width-sm), 0.0625em);
  --arc-internal-inputmask-border: var(--arc-internal-inputmask-border-width) solid var(--arc-internal-inputmask-border-color);


  &:has(:disabled) {
    --arc-internal-inputmask-cursor: not-allowed;
  }

  &:has(:focus:focus-visible) {
    --arc-internal-inputmask-outline: var(--arc-internal-inputmask-border-width) solid var(--arc-color-border-focus);
  }
}

[arc-input-mask-wrapper] {
  display: flex;
  align-items: center;
  justify-content: center;
  max-height: 1lh;
}

[arc-input-mask] {
  position: relative;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: var(--arc-internal-inputmask-width);
  height: var(--arc-internal-inputmask-height);
  opacity: var(--arc-internal-inputmask-opacity, var(--arc-alpha-100));

  color: var(--arc-internal-inputmask-color, transparent);
  background: var(--arc-internal-inputmask-background);
  border: var(--arc-internal-inputmask-border);
  border-radius: var(--arc-internal-inputmask-border-radius);
  text-shadow: var(--arc-internal-inputmask-text-shadow, unset);
  translate: var(--arc-internal-inputmask-translate, unset);

  outline: var(--arc-internal-inputmask-outline, none);
  outline-offset: var(--arc-internal-inputmask-border-width);

  transition-property: opacity, background, color, border-color, outline;
  transition-duration: 100ms;
  transition-timing-function: ease-in-out;
  transition-behavior: allow-discrete;

  [arc-input-type] {
    flex: none;
    outline: none;
    appearance: none;
    position: absolute;
    inset: calc(var(--arc-internal-inputmask-border-width) * -1);
    cursor: var(--arc-internal-inputmask-cursor, pointer);
    margin: 0;
    padding: 0;
    border: 0;
    z-index: var(--arc-internal-input-z-index, unset);
  }

}

}
`,Yr=({type:a,...r})=>(0,o.jsx)("span",{"arc-input-mask-wrapper":"",children:(0,o.jsx)("span",{...r,"arc-input-mask":a})});x("ArcBooleanInputMask",_t);const Et=m`
@layer arc-components {

[arc-checkbox] {

  /* SIZES ---------------------------------------------------------• */

  &[data-size=inherit] { /* font-size base or parent context @ 100% */
    --arc-inherit-checkbox-font-size: 1em;
    --arc-inherit-checkbox-gap: 0.5em;
    --arc-inherit-checkbox-size: 1.125em; /* 18px @ 100% */
    --arc-inherit-checkbox-icon-size: 0.625em; /* 10px @ 100% */
  }

  &[data-size=md] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-checkbox-gap: 8;
    --arc-internal-checkbox-size: 18;
    --arc-internal-checkbox-icon-size: 10;
  }

  &[data-size=sm] {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-checkbox-gap: 6;
    --arc-internal-checkbox-size: 16;
    --arc-internal-checkbox-icon-size: 10;
  }


  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-checkbox-font-size: var(--arc-inherit-checkbox-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-checkbox-gap:  var(--arc-inherit-checkbox-gap, calc(var(--arc-internal-checkbox-gap) / var(--arc-modifier-em)));
    --arc-computed-checkbox-size:  var(--arc-inherit-checkbox-size, calc(var(--arc-internal-checkbox-size) / var(--arc-modifier-em)));
    --arc-computed-checkbox-icon-size:  var(--arc-inherit-checkbox-icon-size, calc(var(--arc-internal-checkbox-icon-size) / var(--arc-modifier-em)));


    /* Inter-Component Communication -------------------------------• */

    /* for: ArcBooleanInputMask */
    --arc-internal-inputmask-color: var(--arc-color-alpha-white-0);
    --arc-internal-inputmask-background: var(--arc-color-alpha-white-50);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-40);
    --arc-internal-inputmask-border-radius: 0.25em; /* 4px @ 100% */
    --arc-internal-inputmask-size: var(--arc-computed-checkbox-size);
    --arc-internal-inputmask-icon-size: var(--arc-computed-checkbox-icon-size);
    --arc-internal-inputmask-width: var(--arc-computed-checkbox-size);
    --arc-internal-inputmask-height: var(--arc-computed-checkbox-size);
    --arc-internal-inputmask-translate: 0 -0.0625em; /* 1px @ 100% */
    --arc-internal-inputmask-text-shadow: 0 0.025em 0 currentcolor;

    /* for: ArcLabel */
    --arc-label-gap: var(--arc-computed-checkbox-gap);
    --arc-label-align-items: stretch; /* ArcLabel • this allows the mask
                                       to stay inline with the label text
                                       regardless of applied line-height */
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-checkbox]:not([hidden]) {
  display: var(--arc-checkbox-display, inline-flex);
  font-size: var(--arc-computed-checkbox-font-size);
  min-height: 1lh;

  &:not(:has(:disabled, :checked, :indeterminate)) {
    &:hover {
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-10);
      --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);
    }
  }

  &:has(:disabled) {
    &:not(:has(:checked, :indeterminate)) {
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-05);
      --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-20);
    }
  }

  &:has(:checked, :indeterminate) {
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);

    &:not(:has(:disabled)):hover {
      --arc-internal-inputmask-opacity: var(--arc-alpha-60);
    }

    &[data-variant=primary] {
      --arc-internal-inputmask-color: var(--arc-color-alpha-white-100);
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-100);

      &:has(:disabled) {
        --arc-internal-inputmask-opacity: var(--arc-alpha-30);
      }
    }

    &[data-variant=secondary] {
      --arc-internal-inputmask-color: var(--arc-color-alpha-black-100);
      --arc-internal-inputmask-background: var(--arc-color-alpha-white-50);

      &:has(:disabled) {
        --arc-internal-inputmask-color: var(--arc-color-alpha-black-30);
        --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-20);
      }
    }
  }
}

}
`,jt=h(P,["hug","fill"]),$t=h(_,["inherit","md","sm"]),Tt=h($,["primary","secondary"]),Ma=({children:a,label:r,layout:e,size:t="md",variant:n="primary",useMask:i=!0,...c})=>{const{className:s,title:l,...d}=c,p={className:s,title:l},v=a??r,u=(0,o.jsx)(wr,{...d,type:"checkbox"}),f=i&&(0,o.jsxs)(Yr,{type:"checkbox",children:[u,(0,o.jsx)(w,{icon:d?.indeterminate?"fa-solid fa-minus fa-fw":"fa-solid fa-check fa-fw"})]}),k=!!v&&(0,o.jsxs)(Q,{layout:e,children:[f||u,(0,o.jsx)(rr,{children:v})]});return(0,o.jsx)("span",{...p,"arc-checkbox":"","data-size":t,"data-variant":n,children:k||f||u})};x("ArcCheckbox",Et);const It=m`
@layer arc-components {

[arc-checkbox-bar] {

  /* --arc-internal/modifier/computed • NOT FOR CONSUMER USE -------• */

  --arc-internal-cb-space: 2;
  --arc-internal-cb-space-subtract: 0;
  --arc-internal-cb-border-radius: 8;
  --arc-internal-cb-background: var(--arc-color-alpha-black-0);

  --arc-internal-cbo-color: var(--arc-color-text-secondary);
  --arc-internal-cbo-background: var(--arc-color-alpha-white-0);
  --arc-internal-cbo-border-color: var(--arc-color-alpha-black-0);
  --arc-internal-cbo-border-width: 1;
  --arc-internal-cbo-border-width-subtract: 2;

  --arc-modifier: var(--arc-checkboxbar-modifier, 14); /* used for size calculations */
  --arc-modifier-px: var(--arc-modifier) * 1px;
  --arc-modifier-em: var(--arc-modifier) * 1em;


  /* SIZES ---------------------------------------------------------- */

  &[data-size=lg] { /* @default */
    --arc-internal-cbo-size: 40;
    --arc-internal-cbo-icon-padding: 8;
    --arc-internal-cbo-text-padding: 12;
  }

  &[data-size=md] {
    --arc-internal-cbo-size: 32;
    --arc-internal-cbo-icon-padding: 6;
    --arc-internal-cbo-text-padding: 8;
  }


  /* COMPUTED --------------------------------------------------------- */

  & {
    --arc-computed-cb-font-size: calc(var(--arc-modifier-px)); /* 14px */
    --arc-computed-cb-space: calc(var(--arc-internal-cb-space) / var(--arc-modifier-em));
    --arc-computed-cb-border-radius: calc(var(--arc-internal-cb-border-radius) / var(--arc-modifier-em));

    --arc-computed-cbo-size: calc((var(--arc-internal-cbo-size) - var(--arc-internal-cb-space-subtract)) / var(--arc-modifier-em));
    --arc-computed-cbo-line-height: calc((var(--arc-internal-cbo-size) - var(--arc-internal-cbo-border-width-subtract)) / var(--arc-modifier));
    --arc-computed-cbo-border-radius: calc((var(--arc-internal-cb-border-radius) - var(--arc-internal-cb-space)) / var(--arc-modifier-em));
    --arc-computed-cbo-border-width: calc(var(--arc-internal-cbo-border-width) / var(--arc-modifier-em));
    --arc-computed-cbo-icon-padding: calc(var(--arc-internal-cbo-icon-padding) / var(--arc-modifier-em));
    --arc-computed-cbo-text-padding: calc(var(--arc-internal-cbo-text-padding) / var(--arc-modifier-em));
  }


  /* LAYOUTS -------------------------------------------------------- */

  &[data-layout=hug] { /* @default */
    --arc-internal-cb-display: inline-flex;
  }

  &[data-layout=fill] {
    --arc-internal-cb-display: flex;
  }
}


[arc-checkbox-bar-option] {

  /* Inter-Component Communication ---------------------------------• */

  --arc-label-align-items: stretch; /* control ArcLabel's vertical footprint;
                                       allows long label text to wrap
                                       without compromising option's
                                       visual border */


  /* STATES --------------------------------------------------------- */

  &:not(:has(:disabled, :checked)) {
    &:hover {
      --arc-internal-cbo-color: var(--arc-color-text-secondary);
      --arc-internal-cbo-background: var(--arc-color-alpha-black-05);
      --arc-internal-cbo-border-color: var(--arc-color-alpha-black-0);
    }
  }

  &:has(:checked) {
    --arc-internal-cbo-color: var(--arc-color-text-primary);
    --arc-internal-cbo-background: var(--arc-color-alpha-black-05);
    --arc-internal-cbo-border-color: var(--arc-color-alpha-black-0);
    --arc-internal-cbo-font-weight: var(--arc-font-weight-500);
  }

  &:has(:focus-within:focus-visible) {
    --arc-internal-cbo-outline: var(--arc-computed-cbo-border-width) solid var(--arc-color-border-focus);
  }
}


/* CORE STYLES ------------------------------------------------------ */

[arc-checkbox-bar-option] {
  flex: 1 1 auto;
  display: flex;
  position: relative;
}

[arc-checkbox-bar] {
  font-size: var(--arc-computed-cb-font-size);
  display: var(--arc-internal-cb-display);
  gap: var(--arc-computed-cb-space);
  border-radius: var(--arc-computed-cb-border-radius);
  background: var(--arc-internal-cb-background);

  padding: 0;
  margin: 0;


  /* Inter-Component Communication ---------------------------------• */

  [arc-input-type=checkbox] {
    opacity: var(--arc-alpha-0);
    appearance: none;
    pointer-events: none;
    position: absolute;
  }

  [arc-checkbox] {
    flex: 1;
    display: flex;
    justify-content: center;
    border-radius: var(--arc-computed-cbo-border-radius);
    outline: var(--arc-internal-cbo-outline, none);
    outline-offset: var(--arc-computed-cbo-border-width);
  }

  [arc-label] {
    flex: 1;
    line-height: var(--arc-computed-cbo-line-height);
    overflow: hidden;
  }

  [arc-label-content] {
    flex: 1;
    display: flex;
    align-items: safe center;
    justify-content: center;

    height: var(--arc-computed-cbo-size);
    min-width: var(--arc-computed-cbo-size);

    border-radius: var(--arc-computed-cbo-border-radius);
    border: var(--arc-computed-cbo-border-width) solid var(--arc-internal-cbo-border-color);
    background: var(--arc-internal-cbo-background);
    color: var(--arc-internal-cbo-color);
    font-weight: var(--arc-internal-cbo-font-weight, unset);

    transition-property: border, background, color, font-weight;
    transition-duration: 200ms;
    transition-timing-function: ease-out;

    &:has([data-element=text]) {
      padding: 0 var(--arc-computed-cbo-text-padding);
    }

    &:not(:has([data-element=text])) {
      padding: 0 var(--arc-computed-cbo-icon-padding);
    }
  }

  [arc-icon-text] [data-element] {
    min-width: min-content;
    overflow: hidden;
    overflow-wrap: break-word;
  }
}

}
`,Rt=h(P,["hug","fill"]),Lt=h(_,["lg","md"]),Pt=({children:a,label:r,icon:e,...t})=>{const n=a||(0,o.jsx)(pr,{icon:e,text:r});return(0,o.jsx)("li",{"arc-checkbox-bar-option":"","data-is-checked":t?.checked||void 0,children:(0,o.jsx)(Ma,{...t,label:n,layout:"fill",useMask:!1})})},Mt=({children:a,size:r="lg",layout:e="hug",...t})=>(0,o.jsx)("ul",{...t,"arc-checkbox-bar":"","data-size":r,"data-layout":e,role:"group",children:a});x("ArcCheckboxBar",It);const Ot=a=>(0,o.jsx)(W,{layout:"icon",size:"md",variant:"tertiary",...a}),Bt=m`
@layer arc-components {

[arc-link] {

  /* VARIANTS ------------------------------------------------------• */

  &[data-variant=inherit] { /* @default */
    --arc-internal-link-icon-color: currentcolor;
    --arc-internal-link-icon-color-hover: color-mix(in srgb, currentcolor, transparent 38.75%);
    --arc-internal-link-icon-color-disabled: color-mix(in srgb, currentcolor, transparent 70%);

    --arc-internal-link-text-color: currentcolor;
    --arc-internal-link-text-color-hover: color-mix(in srgb, currentcolor, transparent 38.75%);
    --arc-internal-link-text-color-disabled: color-mix(in srgb, currentcolor, transparent 70%);

    --arc-internal-link-focus-color: var(--arc-color-border-focus);
  }

  &[data-variant=primary] {
    --arc-internal-link-icon-color: var(--arc-color-icon-primary);
    --arc-internal-link-text-color: var(--arc-color-text-primary);
    --arc-internal-link-icon-color-hover: var(--arc-color-icon-primary-hover);
    --arc-internal-link-text-color-hover: var(--arc-color-text-primary-hover);
    --arc-internal-link-icon-color-disabled: var(--arc-color-icon-disabled);
    --arc-internal-link-text-color-disabled: var(--arc-color-text-disabled);
    --arc-internal-link-focus-color: var(--arc-color-border-focus);
  }

  &[data-variant=secondary] {
    --arc-internal-link-icon-color: var(--arc-color-icon-secondary);
    --arc-internal-link-text-color: var(--arc-color-text-secondary);
    --arc-internal-link-icon-color-hover: var(--arc-color-icon-secondary-hover);
    --arc-internal-link-text-color-hover: var(--arc-color-text-secondary-hover);
    --arc-internal-link-icon-color-disabled: var(--arc-color-icon-disabled);
    --arc-internal-link-text-color-disabled: var(--arc-color-text-disabled);
    --arc-internal-link-focus-color: var(--arc-color-border-focus);
  }

  &[data-variant=inverse] {
    --arc-internal-link-icon-color: var(--arc-color-icon-inverse);
    --arc-internal-link-text-color: var(--arc-color-text-inverse);
    --arc-internal-link-icon-color-hover: var(--arc-color-icon-inverse-hover);
    --arc-internal-link-text-color-hover: var(--arc-color-text-inverse-hover);
    --arc-internal-link-icon-color-disabled: var(--arc-color-icon-inverse-disabled);
    --arc-internal-link-text-color-disabled: var(--arc-color-text-inverse-disabled);
    --arc-internal-link-focus-color: var(--arc-color-border-focus-inverse);
  }

  & {
    --arc-internal-link-outline-size: 0.125em; /* 2px */
    --arc-internal-link-outline-offset: 0.0625em; /* 1px */
  }


  /* STATES --------------------------------------------------------• */

  &:not([data-is-disabled]):hover {
    --arc-internal-link-text-color: var(--arc-link-color-hover, var(--arc-internal-link-text-color-hover));
    --arc-internal-link-icon-color: var(--arc-link-color-hover, var(--arc-internal-link-icon-color-hover));
  }

  &:where(:focus, :focus-within):focus-visible {
    --arc-internal-link-outline: var(--arc-internal-link-outline-size) solid var(--arc-link-color-focus, var(--arc-internal-link-focus-color));
    --arc-internal-link-outline-offset: var(--arc-internal-link-outline-offset);

    * {
      outline: none;
    }
  }

  &[data-is-disabled] {
    --arc-internal-link-text-color: var(--arc-link-color-disabled, var(--arc-internal-link-text-color-disabled));
    --arc-internal-link-icon-color: var(--arc-link-color-disabled, var(--arc-internal-link-icon-color-disabled));
    --arc-internal-link-cursor: not-allowed;
  }


  & {

    /* Inter-Component Communication -------------------------------• */
    --arc-icon-color: var(--arc-link-color, var(--arc-internal-link-icon-color));  /** ArcIcon color */

  }

}


/* CORE STYLES ------------------------------------------------------ */

[arc-link] {
  color: var(--arc-link-color, var(--arc-internal-link-text-color));
  border-radius: var(--arc-border-radius-sm); /* for focus outline */
  text-decoration: var(--arc-link-text-decoration, underline);

  [data-element=icon] {
    text-decoration: none;
  }

  [data-element=text] {
    text-decoration: var(--arc-link-text-decoration, underline);
    text-decoration-color: currentcolor;
  }
}

}
`,Ht=h(P,["gutter","inline"]),Vt=h(_,["inherit","md","sm"]),Nt=h($,["inherit","primary","secondary","inverse"]),Ut=({children:a,layout:r="gutter",size:e="inherit",variant:t="inherit",tag:n="a",href:i,icon:c,text:s,isFlipped:l,isDisabled:d,...p})=>(0,o.jsx)(n,{...p,href:d?void 0:i,"arc-link":"","data-variant":t,"aria-disabled":d,"data-is-disabled":d?"":void 0,children:a||(0,o.jsx)(pr,{icon:c,text:s,size:e,layout:r,isFlipped:l})});x("ArcLink",Bt);const Oa=[...Tr],Ba=h($,["common"]),Ft=h($,["ai","brand"]),Ha=["top-start","top-center","top-end","left-start","right-start","left-center","center","right-center","left-end","right-end","bottom-start","bottom-center","bottom-end"],Va=["ignore","flip"],b={Threshold:0,StartX:{align:"start",y:"y1",oY:"ch",xMod:-1,yMod:1},StartY:{align:"start",x:"x1",oX:"cw",xMod:-1,yMod:1},CenterX:{align:"center",y:"cy",oY:"z",xMod:-1,yMod:1},CenterY:{align:"center",x:"cx",oX:"z",xMod:1,yMod:-1},EndX:{align:"end",y:"y2",oY:"ch",xMod:1,yMod:-1},EndY:{align:"end",x:"x2",oX:"cw",xMod:1,yMod:-1},Top:{edge:"top",y:"top",pX:"z",pY:"ch",oY:"z",pMod:-1},Bottom:{edge:"bottom",y:"bottom",pX:"z",pY:"ch",oY:"z",pMod:1},Right:{edge:"right",x:"right",pX:"cw",pY:"z",oX:"z",pMod:1},Left:{edge:"left",x:"left",pX:"cw",pY:"z",oX:"z",pMod:-1},Center:{edge:"center",pX:"cw",pY:"ch",pMod:0}},T=(a,r,e)=>e==="left"&&a.a.left>=a.v.threshold&&a.a.left-a.p.width+r.x>=a.v.threshold||e==="top"&&a.a.top>=a.v.threshold&&a.a.top-a.p.height+r.y>=a.v.threshold||e==="right"&&a.a.right<=a.v.width-a.v.threshold&&a.a.right+a.p.width+r.x<=a.v.width-a.v.threshold||e==="bottom"&&a.a.bottom<=a.v.height-a.v.threshold&&a.a.bottom+a.p.height+r.y<=a.v.height-a.v.threshold,Na=(a,r)=>a.a.top+a.p.height+r.y<=a.v.height-a.v.threshold&&a.a.top>=a.v.threshold,Ua=(a,r)=>a.a.left+a.p.width+r.x<=a.v.width-a.v.threshold&&a.a.left>=a.v.threshold,Fa=(a,r)=>a.a.cy+a.p.ch+r.y<=a.v.height-a.v.threshold&&a.a.cy-a.p.ch+r.y>=a.v.threshold,Da=(a,r)=>a.a.cx+a.p.cw+r.x<=a.v.width-a.v.threshold&&a.a.cx-a.p.cw+r.x>=a.v.threshold,Ya=(a,r)=>a.a.bottom<=a.v.height-a.v.threshold&&a.a.bottom-a.p.height+r.y>=a.v.threshold,Za=(a,r)=>a.a.right<=a.v.width-a.v.threshold&&a.a.right-a.p.width+r.x>=a.v.threshold,vr={"top-start":{...b.Top,...b.StartY,fitCheck:(a,r)=>T(a,r,b.Top.edge)&&Ua(a,r),flip:"bottom-start"},"top-center":{...b.Top,...b.CenterY,fitCheck:(a,r)=>T(a,r,b.Top.edge)&&Da(a,r),flip:"bottom-center"},"top-end":{...b.Top,...b.EndY,fitCheck:(a,r)=>T(a,r,b.Top.edge)&&Za(a,r),flip:"bottom-end"},"right-start":{...b.Right,...b.StartX,fitCheck:(a,r)=>T(a,r,b.Right.edge)&&Na(a,r),flip:"left-start"},"right-center":{...b.Right,...b.CenterX,fitCheck:(a,r)=>T(a,r,b.Right.edge)&&Fa(a,r),flip:"left-center"},"right-end":{...b.Right,...b.EndX,fitCheck:(a,r)=>T(a,r,b.Right.edge)&&Ya(a,r),flip:"left-end"},center:{...b.Center,...b.CenterX,...b.CenterY,fitCheck:(a,r)=>a.a.cx-a.p.cw+r.x>=a.v.threshold&&a.a.cx+a.p.cw+r.x<=a.v.width-a.v.threshold&&a.a.cy-a.p.ch+r.y>=a.v.threshold&&a.a.cy+a.p.ch+r.y<=a.v.height-a.v.threshold,flip:"center"},"bottom-start":{...b.Bottom,...b.StartY,fitCheck:(a,r)=>T(a,r,b.Bottom.edge)&&Ua(a,r),flip:"top-start"},"bottom-center":{...b.Bottom,...b.CenterY,fitCheck:(a,r)=>T(a,r,b.Bottom.edge)&&Da(a,r),flip:"top-center"},"bottom-end":{...b.Bottom,...b.EndY,fitCheck:(a,r)=>T(a,r,b.Bottom.edge)&&Za(a,r),flip:"top-end"},"left-start":{...b.Left,...b.StartX,fitCheck:(a,r)=>T(a,r,b.Left.edge)&&Na(a,r),flip:"right-start"},"left-center":{...b.Left,...b.CenterX,fitCheck:(a,r)=>T(a,r,b.Left.edge)&&Fa(a,r),flip:"right-center"},"left-end":{...b.Left,...b.EndX,fitCheck:(a,r)=>T(a,r,b.Left.edge)&&Ya(a,r),flip:"right-end"}},M={popover:Oa[0],anchorAt:Ha[10],boundary:Va[0],variant:Ba[0],isInverse:!1,hasTail:!1,tailSize:10,threshold:0,offset:0},Dt=(a,r="arc-popover")=>a||N(r),Yt=(a=M.threshold)=>({threshold:a,width:document.documentElement.clientWidth,height:document.documentElement.clientHeight,scrollX:window.scrollX,scrollY:window.scrollY,left:window.scrollX,top:window.scrollY,right:window.scrollX+document.documentElement.clientWidth,bottom:window.scrollY+document.documentElement.clientHeight}),Xa=a=>({...JSON.parse(JSON.stringify(a)),cw:a.width/2,ch:a.height/2,cx:a.left+a.width/2,cy:a.top+a.height/2,x1:a.left,x2:a.left+a.width,y1:a.top,y2:a.top+a.height,z:0}),Zt=(a,r,e=M.offset,t=M.tailSize,n=M.threshold)=>({a:Xa(a?.getBoundingClientRect()??new DOMRect),p:Xa(r?.getBoundingClientRect()??new DOMRect),o:e,v:Yt(n),t}),qa=(a,r,e)=>({x:["left","right"].includes(a)?r*e:0,y:["top","bottom"].includes(a)?r*e:0}),Xt=(a,r)=>[...Object.keys(vr)].filter(e=>D(e,a)&&D(r.align,"center")?!e.includes("center"):e).sort((e,t)=>{const n=br(vr[e].edge,r.edge),i=br(vr[t].edge,r.edge),c=n?-1:1;return br(n,i)?0:c}),Wa=({anchorAt:a,measurements:r})=>{const e=vr[a],t=qa(e.edge,r.o,e.pMod);return{cx:r.a.cx,cy:r.a.cy,offsets:t,config:e}},qt=({anchorAt:a,measurements:r})=>{const{offsets:e,config:t}=Wa({anchorAt:a,measurements:r});return t.fitCheck(r,e)?a:[t.flip,...Xt(a,t)].find(n=>{const i=vr[n],c=qa(i.edge,r.o,i.pMod);return i.fitCheck(r,c)})??a},V=(a,r="px")=>`${a}${r}`,Zr=({anchorElement:a,popoverElement:r,anchorAt:e=M.anchorAt,boundary:t,offset:n,tailSize:i,threshold:c=b.Threshold})=>{const s=e,l=Zt(a,r,n,i,c),d=t==="flip"?qt({anchorAt:s,measurements:l}):s,{cx:p,cy:v}=Wa({anchorAt:d,measurements:l}),u={cx:p,cy:v},f=a?.dataset?.layout==="icon"||l.t>0||!e?.includes("-center")&&(e?.includes("left-")||e?.includes("right-"));return{anchorAt:d,vars:{"--js-cx":V((u.cx+window.scrollX).toFixed(2)),"--js-cy":V((u.cy+window.scrollY).toFixed(2)),"--js-w":V(l.a.width.toFixed(2)),"--js-h":V(l.a.height.toFixed(2)),"--js-cw":V(l.a.cw.toFixed(2)),"--js-ch":V(l.a.ch.toFixed(2)),"--js-o":V(l.o),"--js-t":V(l.t,""),"--js-i":f?"1":"0","--js-m":V(ra(l.t,l.a.height,l.a.cw).toFixed(2))}}},Xr=(0,g.forwardRef)((a,r)=>(0,o.jsx)(W,{...a,ref:r,"arc-popover-button":""})),Ka=({isInverse:a,isOpen:r,...e})=>(0,o.jsx)(W,{icon:"fa-regular fa-xmark fa-fw",text:"Dismiss",layout:"icon",shape:"square",size:"sm",variant:a?"tertiary-inverse":"tertiary",...e,"arc-popover-dismiss":""}),Wt=({children:a,showDismiss:r,dismissProps:e,tag:t="header",...n})=>(0,o.jsxs)(t,{...n,"arc-popover-header":"",children:[a,r&&(0,o.jsx)(Ka,{...e})]}),Kt=({tag:a="article",...r})=>(0,o.jsx)(a,{...r,"arc-popover-body":""}),Gt=({showDivider:a,tag:r="footer",...e})=>(0,o.jsx)(r,{...e,"arc-popover-footer":"","has-divider":a?"":void 0}),Jt=a=>(0,o.jsx)("span",{...a,"arc-popover-tail":""});Xr.displayName="ArcPopoverButton";const Qt=m`
  & {
    --arc-modifier: 16; /* unitless base • user agent font-size: 16px @ 100% */
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    /* local spacing 16px base @ 100% */
    --popover-space-1: 0.0625em;
    --popover-space-2: 0.125em;
    --popover-space-3: 0.1875em;
    --popover-space-4: 0.25em;
    --popover-space-6: 0.375em;
    --popover-space-8: 0.5em;
    --popover-space-10: 0.625em;
    --popover-space-12: 0.75em;
    --popover-space-16: 1em;
    --popover-space-20: 1.25em;
    --popover-space-24: 1.5em;

    --popover-outline-size: var(--arc-popover-outline-size, var(--popover-space-2));
    --popover-trail-mix: 50%; /* mix ratio for tail outline color (for gradient along the popover perimeter), e.g.: dropdown[variant=ai-outline] */

    /* tail metrics */
    --popover-computed-font-size: calc(14 / var(--arc-modifier-em));
    --popover-tail-base: (var(--js-t, 10) / var(--arc-modifier-em));
    --popover-tail-size: (var(--popover-tail-base) + var(--popover-outline-size));
    --popover-tail-shift: (var(--popover-tail-base) * 0.707 * var(--js-i)); /* 0.707 = sqrt(2)/2 = multiplier for half of rotated tail size */

    /* offset */
    --popover-offset-x: calc(var(--js-cw) + var(--js-o) + var(--arc-popover-nudge, 0em));
    --popover-offset-y: calc(var(--js-ch) + var(--js-o) + var(--arc-popover-nudge, 0em));
    --popover-shift-x: calc(var(--js-cw) + var(--popover-tail-shift));
    --popover-shift-y: calc(var(--js-ch) + var(--popover-tail-shift));

    /* padding */
    --popover-padding: var(--popover-space-16);
    --popover-header-gap: var(--popover-space-12);
    --popover-footer-gap: var(--popover-space-12);

    &:has([arc-popover-body]) {
      --popover-padding: 0;

      &:has([arc-popover-header]) {
        --popover-body-padding-block-start: 0;
      }

      &:has([arc-popover-footer]:not([has-divider])) {
        --popover-body-padding-block-end: 0;
      }
    }
  }

  &:has([arc-popover-tail]) {
    --popover-tail-nudge: var(--popover-outline-size);
    --popover-tail-radius: var(--popover-outline-size);
    --popover-tail-clamp: clamp(
      var(--popover-tail-shift),
      var(--js-h) - var(--popover-tail-shift) * 2,
      var(--js-cw) - var(--popover-outline-size) * 2
    );
  }
`,rn=m`
  &:where([arc-popover*=top], [arc-popover*=bottom], [arc-popover=center]) {
    --arc-popover-min-width: calc(var(--js-w) + var(--popover-tail-shift) * 2);
    --arc-popover-min-height: auto;
  }

  &:where(
    [arc-popover=right-start],
    [arc-popover=right-end],
    [arc-popover=left-start],
    [arc-popover=left-end]
  ) {
    --arc-popover-min-height: calc(var(--js-ch) + var(--popover-tail-shift) * 2);
  }

  &:where([arc-popover*=top]) {
    --popover-tr: var(--popover-tail-radius) 0;
    --popover-ty: calc(-100% - var(--popover-offset-y));
    --popover-tail-ty: 50%;

    &:where([arc-popover*=-start]) {
      --arc-trail-mix: 87.5%;
      --popover-tx: calc(0% - var(--popover-shift-x));
      --popover-tail-inset: auto auto var(--popover-tail-nudge) calc(0% + var(--popover-tail-clamp));
      --popover-tail-tx: 50%;
    }

    &:where([arc-popover*=-center]) {
      --arc-trail-mix: 75%;
      --popover-tx: -50%;
      --popover-tail-inset: auto auto var(--popover-tail-nudge) 50%;
      --popover-tail-tx: -50%;
    }

    &:where([arc-popover*=-end]) {
      --arc-trail-mix: 62.5%;
      --popover-tx: calc(-100% + var(--popover-shift-x));
      --popover-tail-inset: auto auto var(--popover-tail-nudge) calc(100% - var(--popover-tail-clamp));
      --popover-tail-tx: -150%;
    }
  }

  &:where([arc-popover*=right]) {
    --popover-tr: 0 var(--popover-tail-radius);
    --popover-tx: calc(0% + var(--popover-offset-x));
    --popover-tail-tx: -50%;

    &:where([arc-popover*=-start]) {
      --arc-trail-mix: 62.5%;
      --popover-ty: calc(0% - var(--popover-shift-y));
      --popover-tail-ty: calc(50% + var(--popover-shift-y));
      --popover-tail-inset: auto auto 100% var(--popover-tail-nudge);
    }

    &:where([arc-popover*=-center]) {
      --arc-trail-mix: 75%;
      --popover-ty: -50%;
      --popover-tail-ty: 50%;
      --popover-tail-inset: auto auto 50% var(--popover-tail-nudge);
    }

    &:where([arc-popover*=-end]) {
      --arc-trail-mix: 87.5%;
      --popover-ty: calc(-100% + var(--popover-shift-y));
      --popover-tail-ty: calc(50% - var(--popover-shift-y));
      --popover-tail-inset: auto auto 0% var(--popover-tail-nudge);
    }
  }

  &:where([arc-popover=center]) {
    --arc-trail-mix: 50%;
    --popover-tx: -50%;
    --popover-ty: -50%;
    --popover-tail-size: 0em;
    --popover-tail-display: none;
    --popover-tail-display-duration: 0ms;
  }

  &:where([arc-popover*=bottom]) {
    --popover-tr: var(--popover-tail-radius) 0;
    --popover-ty: calc(0% + var(--popover-offset-y));
    --popover-tail-ty: -50%;

    &:where([arc-popover*=-start]) {
      --arc-trail-mix: 37.5%;
      --popover-tx: calc(0% - var(--popover-shift-x));
      --popover-tail-tx: 50%;
      --popover-tail-inset: var(--popover-tail-nudge) auto auto calc(0% + var(--popover-tail-clamp));
    }

    &:where([arc-popover*=-center]) {
      --arc-trail-mix: 25%;
      --popover-tx: -50%;
      --popover-tail-tx: -50%;
      --popover-tail-inset: var(--popover-tail-nudge) auto auto 50%;
    }

    &:where([arc-popover*=-end]) {
      --arc-trail-mix: 12.5%;
      --popover-tx: calc(-100% + var(--popover-shift-x));
      --popover-tail-tx: -150%;
      --popover-tail-inset: var(--popover-tail-nudge) auto auto calc(100% - var(--popover-tail-clamp));
    }
  }

  &:where([arc-popover*=left]) {
    --popover-tr: 0 var(--popover-tail-radius);
    --popover-tx: calc(-100% - var(--popover-offset-x));
    --popover-tail-tx: 50%;

    &:where([arc-popover*=-start]) {
      --arc-trail-mix: 12.5%;
      --popover-ty: calc(0% - var(--popover-shift-y));
      --popover-tail-ty: calc(50% + var(--popover-shift-y));
      --popover-tail-inset: auto var(--popover-tail-nudge) 100% auto;
    }

    &:where([arc-popover*=-center]) {
      --arc-trail-mix: 25%;
      --popover-ty: -50%;
      --popover-tail-ty: 50%;
      --popover-tail-inset: auto var(--popover-tail-nudge) 50% auto;
    }

    &:where([arc-popover*=-end]) {
      --arc-trail-mix: 37.5%;
      --popover-ty: calc(-100% + var(--popover-shift-y));
      --popover-tail-ty: calc(50% - var(--popover-shift-y));
      --popover-tail-inset: auto var(--popover-tail-nudge) 0% auto;
    }
  }

`,an=m`
  /* default variant: common */
  &:where([variant], [variant=common]) {
    --popover-color: var(--arc-color-text-primary);
    --popover-background: var(--arc-color-background-primary);
    --popover-footer-divider-color: var(--arc-color-border-standard);
    --popover-outline-color: var(--popover-background);
    --popover-tail-background: var(--popover-background);
    --popover-tail-outline-color: var(--popover-outline-color);
    --popover-tail-border: solid var(--popover-outline-size) var(--popover-tail-outline-color);
    --popover-shadow: var(--arc-shadow-light-lg);
  }

  &:where([variant=common][is-inverse]) {
    --popover-color: var(--arc-color-text-inverse);
    --popover-background: var(--arc-color-background-inverse);
    --popover-shadow: var(--arc-shadow-dark-lg);
    --popover-footer-divider-color: var(--arc-color-alpha-white-20);
  }

  &:where([variant=brand]) {
    --popover-background: var(--arc-color-brand-light);
    --popover-outline-color: var(--arc-color-border-brand);
  }

  &:where([variant=ai]) {
    --popover-background: var(--arc-color-ai-blue-050) var(--arc-gradient-ai-050);
    --popover-outline-color: var(--arc-gradient-ai-200);
    --popover-tail-background: color-mix(in srgb,
      var(--arc-color-ai-purple-050) var(--arc-trail-mix, 37.5%),
      var(--arc-color-ai-blue-050) clamp(0%, 100% - var(--arc-trail-mix, 37.5%), 100%)
    );
    --popover-tail-outline-color: color-mix(in srgb,
      var(--arc-color-ai-purple-200) var(--arc-trail-mix, 37.5%),
      var(--arc-color-ai-blue-200) clamp(0%, 100% - var(--arc-trail-mix, 37.5%), 100%)
    );
  }
`,en=m`
  [arc-popover] {
    position: absolute;
    inset: var(--js-cy, 50%) auto auto var(--js-cx, 50%);
    translate: var(--popover-tx) var(--popover-ty);
    overflow: visible; /* For the tail – content overflow is set on '[arc-popover-content]' */

    border: none;
    border-radius: calc(var(--arc-popover-border-radius, var(--arc-border-radius-lg)) + var(--popover-outline-size));
    box-shadow: var(--arc-popover-shadow, var(--popover-shadow));
    background: var(--arc-popover-outline-color, var(--popover-outline-color));
    padding: var(--popover-outline-size);
    margin: 0;

    &::backdrop {
      background: var(--arc-popover-backdrop, transparent);
    }

    :where([arc-popover-content]) {
      flex: var(--arc-popover-flex, 1);
      position: relative;
      display: var(--arc-popover-content-display, flex);
      flex-direction: var(--arc-popover-content-flex-direction, column);
      gap: var(--arc-popover-content-gap, 0);

      overflow: var(--arc-popover-overflow, auto);
      overscroll-behavior: var(--arc-popover-overscroll-behavior, contain);

      color: var(--arc-popover-color, var(--popover-color));
      background: var(--arc-popover-background, var(--popover-background));
      border-radius: var(--arc-popover-border-radius, var(--arc-border-radius-lg));
      padding: var(--arc-popover-padding, var(--popover-padding));

      width: var(--arc-popover-width, fit-content);
      height: var(--arc-popover-height, fit-content);
      min-width: var(--arc-popover-min-width, none);
      min-height: var(--arc-popover-min-height, none);
      max-width: var(--arc-popover-max-width, 100vw);
      max-height: var(--arc-popover-max-height, 65vh);
      resize: var(--arc-popover-resize, none);
      font-size: var(--arc-popover-font-size, var(--popover-computed-font-size));

      /* :where([arc-popover-header], [arc-popover-body], [arc-popover-footer]) > *{} */
    }

    :where([arc-popover-dismiss]) {
      align-self: var(--arc-popover-dismiss-align-self, auto);
      display: var(--arc-popover-dismiss-display, inline-flex);
      align-items: var(--arc-popover-dismiss-align-items, center);
      justify-content: var(--arc-popover-dismiss-justify-content, center);
      margin:  var(--arc-popover-dismiss-margin, calc(-1 * var(--popover-space-6)) 0);
    }

    :where([arc-popover-header]) {
      flex: var(--arc-popover-header-flex, none);
      display: var(--arc-popover-header-display, flex);
      justify-content: var(--arc-popover-header-justify-content, space-between);
      align-items: var(--arc-popover-header-align-items, baseline);
      gap: var(--arc-popover-header-gap, var(--popover-header-gap));

      padding: var(--arc-popover-header-padding, var(--popover-space-16));

      color: var(--arc-popover-header-color, inherit);
      background: var(--arc-popover-header-background, none);

      &:has([arc-popover-dismiss]) {
        padding-inline-end: var(--popover-space-12);
      }
    }

    :where([arc-popover-body]) {
      flex: var(--arc-popover-body-flex, 1);
      display: var(--arc-popover-body-display, block);
      flex-direction: var(--arc-popover-body-flex-direction, unset);

      padding: var(--arc-popover-body-padding,
        var(--popover-body-padding-block-start, var(--popover-space-16))
        var(--popover-space-16)
        var(--popover-body-padding-block-end, var(--popover-space-16))
        var(--popover-space-16)
      );
    }

    :where([arc-popover-footer]) {
      flex: var(--arc-popover-footer-flex, none);
      display: var(--arc-popover-footer-display, flex);
      flex-direction: var(--arc-popover-footer-flex-direction, row-reverse);
      justify-content: var(--arc-popover-footer-justify-content, space-between);
      align-items: var(--arc-popover-footer-align-items, center);
      gap: var(--arc-popover-footer-gap, var(--popover-footer-gap));

      padding: var(--arc-popover-footer-padding, var(--popover-space-16));

      background: var(--arc-popover-footer-background, none);
      color: var(--arc-popover-footer-color, inherit);

      &:where([has-divider]) {
        border-top: var(--arc-popover-footer-border-top,
          solid
          var(--popover-space-1)
          var(--popover-footer-divider-color)
        );
      }
    }

    :where([arc-popover-tail]) {
      position: absolute;
      display: var(--popover-tail-display, block);
      width: calc(var(--popover-tail-size));
      height: calc(var(--popover-tail-size));
      inset: var(--popover-tail-inset);
      translate: var(--popover-tail-tx) var(--popover-tail-ty);
      rotate: 45deg;

      background: var(--popover-tail-background);
      border: var(--popover-tail-border, none);
      border-radius: var(--popover-tr);
    }
  }

`,kr={SETUP:Qt,POSITION:rn,VARIANTS:an,ELEMENTS:en},tn=m`
@layer arc-components {
  [arc-popover] {
    ${kr.SETUP}
    ${kr.POSITION}
    ${kr.VARIANTS}
  }

  ${kr.ELEMENTS}

  /* POPOVER transition --------------------------------------------• */

  [arc-popover] {
    transition-behavior: allow-discrete, smooth;
    transition-duration: var(--arc-popover-transition-duration, 240ms);
    transition-property: display, opacity, translate;
    transition-timing-function: ease-out;

    &::backdrop {
      transition: inherit;
    }
  }

  @supports (overlay: auto) {
    [arc-popover] {
      transition-behavior: allow-discrete;
      transition-property: display, overlay, opacity, translate;
    }
  }

  /* POPOVER State -------------------------------------------------• */

  [arc-popover] {
    opacity: 0;

    &::backdrop {
      opacity: 0;
    }

    &:popover-open {
      opacity: 1;
    }

    &:popover-open::backdrop {
      opacity: 1;
    }
  }

  @starting-style {
    [arc-popover]:popover-open {
      opacity: 0;
    }

    [arc-popover]:popover-open::backdrop {
      opacity: 0;
    }
  }
}

`,nn=(a,r="toggle")=>{const e=document.getElementById(a);({toggle:()=>e?.togglePopover(),show:()=>e?.showPopover(),hide:()=>e?.hidePopover()})[r]()},qr=({children:a,popover:r=M.popover,alternateAnchor:e,anchorProps:t,anchorAt:n,boundary:i,variant:c=M.variant,isInverse:s,hasTail:l,tailSize:d=M.tailSize,offset:p=M.offset,hidePopoverButton:v,headerSlot:u,showDismiss:f,dismissProps:k,footerSlot:C,showFooterDivider:S,tag:O="div",contentTag:A="div",onToggle:y,...I})=>{const B=(0,g.useRef)(null),U=(0,g.useRef)(null),[R,Ar]=(0,g.useState)(!1),[zr,Cr]=(0,g.useState)(n||M.anchorAt),[lo,ee]=(0,g.useState)({}),[Sr,so]=(0,g.useState)(0),[_r,po]=(0,g.useState)(l?p??0:0),Er=(0,g.useMemo)(()=>({popovertarget:I.id,"aria-haspopup":!0,"aria-controls":I.id,"aria-expanded":R}),[I.id,R]),te=(0,g.useMemo)(()=>({isInverse:s,isOpen:R,"aria-label":"Dismiss",...Er,...k}),[k,s,R,Er]),Kr=(0,g.useMemo)(()=>({ref:B,...Er,...t}),[B,t,Er]),er=(0,g.useMemo)(()=>typeof e=="function"?e(Kr):typeof e=="string"?document.getElementById(e):B.current,[e,Kr]),jr=(0,g.useCallback)(({...L})=>{Cr(L.anchorAt),ee(L.vars)},[Cr,ee]);return(0,g.useEffect)(()=>{const L=l?d??0:0,F=Number(p??0)+L*(Math.sqrt(2)/2);so(L),po(F)},[l,d,p]),(0,g.useEffect)(()=>{const L=U?.current;if(!er)return;let F=null,hr=null,tr=null;const ne=()=>{F=er?.getBoundingClientRect()??{},tr&&aa({a:F,b:hr})?(hr=F,jr(Zr({anchorElement:er,popoverElement:L,anchorAt:n,boundary:i,offset:_r,tailSize:Sr}))):tr=null,tr=requestAnimationFrame(ne)};tr=requestAnimationFrame(ne)},[er,R,n,i,_r,Sr,jr]),(0,g.useEffect)(()=>{const L=U.current;if(!L)return;const F=hr=>{const{newState:tr}=hr;Ar(tr==="open"),jr(Zr({anchorElement:er,popoverElement:L,anchorAt:n,boundary:i,offset:_r,tailSize:Sr})),y?.(hr)};return L.addEventListener("toggle",F),()=>{L.removeEventListener("toggle",F)}},[U,y,er,n,i,_r,Sr,jr]),(0,o.jsxs)(o.Fragment,{children:[!(e&&v)&&(0,o.jsx)(Xr,{...Kr}),(0,o.jsxs)(O,{...I,"arc-popover":zr,ref:U,popover:r,boundary:i,variant:c,"is-inverse":s?"":void 0,"has-tail":l?"":void 0,style:{...I.style,...lo},children:[l&&(0,o.jsx)(Jt,{}),(0,o.jsxs)(A,{"arc-popover-content":"",children:[!!u&&(0,o.jsx)(Wt,{showDismiss:f,dismissProps:te,children:u}),u||C?(0,o.jsx)(Kt,{children:a}):a,!!C&&(0,o.jsx)(Gt,{showDivider:S,children:C})]}),f&&!u&&(0,o.jsx)(Ka,{...te})]})]})};qr.handlePopover=nn,qr.Button=Xr,x("ArcPopover",tn);const on=m`
@layer arc-components {

[arc-radio] {

  /* SIZES ---------------------------------------------------------• */

  &[data-size=inherit] { /* font-size base or parent context @ 100% */
    --arc-inherit-radio-font-size: 1em;
    --arc-inherit-radio-gap: 0.5em;
    --arc-inherit-radio-size: 1.125em; /* 18px @ 100% */
    --arc-inherit-radio-icon-size: 0.625em; /* 10px @ 100% */
  }

  &[data-size=md] {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-radio-gap: 8;
    --arc-internal-radio-size: 18;
    --arc-internal-radio-icon-size: 10;
  }

  &[data-size=sm] {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-radio-gap: 6;
    --arc-internal-radio-size: 16;
    --arc-internal-radio-icon-size: 10;
  }

  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-radio-font-size: var(--arc-inherit-radio-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-radio-gap:  var(--arc-inherit-radio-gap, calc(var(--arc-internal-radio-gap) / var(--arc-modifier-em)));
    --arc-computed-radio-size:  var(--arc-inherit-radio-size, calc(var(--arc-internal-radio-size) / var(--arc-modifier-em)));
    --arc-computed-radio-icon-size:  var(--arc-inherit-radio-icon-size, calc(var(--arc-internal-radio-icon-size) / var(--arc-modifier-em)));


    /* Inter-Component Communication -------------------------------• */

    /* for: ArcBooleanInputMask */
    --arc-internal-inputmask-color: var(--arc-color-alpha-white-0);
    --arc-internal-inputmask-background: var(--arc-color-alpha-white-50);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-40);
    --arc-internal-inputmask-border-radius: var(--arc-border-radius-circle);
    --arc-internal-inputmask-size: var(--arc-computed-radio-size);
    --arc-internal-inputmask-icon-size: var(--arc-computed-radio-icon-size);
    --arc-internal-inputmask-width: var(--arc-computed-radio-size);
    --arc-internal-inputmask-height: var(--arc-computed-radio-size);
    --arc-internal-inputmask-translate: 0 -0.0625em; /* 1px @ 100% */


    /* for: ArcLabel */
    --arc-label-gap: var(--arc-computed-radio-gap);
    --arc-label-align-items: stretch; /* ArcLabel • this allows the mask
                                       to stay inline with the label text
                                       regardless of applied line-height */
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-radio]:not([hidden]) {
  display: var(--arc-radio-display, flex);
  font-size: var(--arc-computed-radio-font-size);
  min-height: 1lh;

  &:not(:has(:disabled, :checked)) {
    &:hover {
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-10);
      --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);
    }
  }

  &:has(:disabled) {
    &:not(:has(:checked)) {
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-05);
      --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-20);
    }
  }

  &:has(:checked) {
    --arc-internal-inputmask-color: var(--arc-color-alpha-white-100);
    --arc-internal-inputmask-background: var(--arc-color-alpha-black-100);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);

    &:not(:has(:disabled)):hover {
      --arc-internal-inputmask-opacity: var(--arc-alpha-60);
    }

    &:has(:disabled) {
      --arc-internal-inputmask-opacity: var(--arc-alpha-30);
    }
  }
}

}
`,cn=h(P,["hug","fill"]),ln=h(_,["inherit","md","sm"]),Ga=({children:a,label:r,layout:e,size:t="md",useMask:n=!0,...i})=>{const{className:c,title:s,...l}=i,d={className:c,title:s},p=a??r,v=(0,o.jsx)(wr,{...l,type:"radio"}),u=n&&(0,o.jsxs)(Yr,{type:"radio",children:[v,(0,o.jsx)(w,{icon:"fa-solid fa-circle-small fa-fw"})]}),f=!!p&&(0,o.jsxs)(Q,{layout:e,children:[u||v,(0,o.jsx)(rr,{children:p})]});return(0,o.jsx)("span",{...d,"arc-radio":"","data-size":t,children:f||u||v})};x("ArcRadio",on);const sn=m`
@layer arc-components {

[arc-radio-bar] {

  /* --arc-internal/modifier/computed • NOT FOR CONSUMER USE -------- */

  --arc-internal-rb-space: 2;
  --arc-internal-rb-space-subtract: 4;
  --arc-internal-rb-space-border-subtract: 6;
  --arc-internal-rb-border-radius: 8;
  --arc-internal-rb-background: var(--arc-color-alpha-black-03);

  --arc-internal-rbo-color: var(--arc-color-text-secondary);
  --arc-internal-rbo-background: var(--arc-color-alpha-white-0);
  --arc-internal-rbo-border-color: var(--arc-color-alpha-black-0);
  --arc-internal-rbo-border-width: 1;
  --arc-internal-rbo-border-width-subtract: 2;

  --arc-modifier: var(--arc-radiobar-modifier, 14); /* used for size calculations */
  --arc-modifier-px: var(--arc-modifier) * 1px;
  --arc-modifier-em: var(--arc-modifier) * 1em;


  /* SIZES ---------------------------------------------------------• */

  &[data-size=lg] { /** @default */
    --arc-internal-rbo-size: 40;
    --arc-internal-rbo-icon-padding: 8;
    --arc-internal-rbo-text-padding: 12;
  }

  &[data-size=md] {
    --arc-internal-rbo-size: 32;
    --arc-internal-rbo-icon-padding: 6;
    --arc-internal-rbo-text-padding: 8;
  }


  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-computed-rb-font-size: calc(var(--arc-modifier-px)); /* 14px */
    --arc-computed-rb-space: calc(var(--arc-internal-rb-space) / var(--arc-modifier-em));
    --arc-computed-rb-border-radius: calc(var(--arc-internal-rb-border-radius) / var(--arc-modifier-em));

    --arc-computed-rbo-size: calc((var(--arc-internal-rbo-size) - var(--arc-internal-rb-space-subtract)) / var(--arc-modifier-em));
    --arc-computed-rbo-line-height: calc((var(--arc-internal-rbo-size) - var(--arc-internal-rb-space-border-subtract)) / var(--arc-modifier));
    --arc-computed-rbo-border-radius: calc((var(--arc-internal-rb-border-radius) - var(--arc-internal-rb-space)) / var(--arc-modifier-em));
    --arc-computed-rbo-border-width: calc(var(--arc-internal-rbo-border-width) / var(--arc-modifier-em));
    --arc-computed-rbo-icon-padding: calc(var(--arc-internal-rbo-icon-padding) / var(--arc-modifier-em));
    --arc-computed-rbo-text-padding: calc(var(--arc-internal-rbo-text-padding) / var(--arc-modifier-em));
  }


  /* LAYOUTS -------------------------------------------------------• */

  &[data-layout=hug] { /** @default */
    --arc-internal-rb-display: inline-flex;
  }

  &[data-layout=fill] {
    --arc-internal-rb-display: flex;
  }
}


[arc-radio-bar-option] {

  /* Inter-Component Communication ---------------------------------• */

  --arc-label-align-items: stretch; /* control ArcLabel's vertical footprint;
                                       allows long label text to wrap
                                       without compromising option's
                                       visual border */


  /* STATES --------------------------------------------------------• */

  &:not(:has(:disabled, :checked)) {
    &:hover {
      --arc-internal-rbo-color: var(--arc-color-text-primary);
      --arc-internal-rbo-background: var(--arc-color-alpha-black-05);
      --arc-internal-rbo-border-color: var(--arc-color-border-standard);
    }
  }

  &:has(:checked) {
      --arc-internal-rbo-color: var(--arc-color-text-primary);
      --arc-internal-rbo-background: var(--arc-color-alpha-white-100);
      --arc-internal-rbo-border-color: var(--arc-color-border-stark);
      --arc-internal-rbo-font-weight: var(--arc-font-weight-500);
  }

  &:has(:focus-within:focus-visible) {
    --arc-internal-rbo-outline: var(--arc-computed-rbo-border-width) solid var(--arc-color-border-focus);
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-radio-bar-option] {
  flex: 1 1 auto;
  display: flex;
  position: relative;
}

[arc-radio-bar] {
  font-size: var(--arc-computed-rb-font-size);
  display: var(--arc-internal-rb-display);
  gap: var(--arc-computed-rb-space);
  border-radius: var(--arc-computed-rb-border-radius);
  background: var(--arc-internal-rb-background);

  padding: var(--arc-computed-rb-space);
  margin: 0;


  /* Inter-Component Communication ---------------------------------• */

  [arc-input-type=radio] {
    opacity: var(--arc-alpha-0);
    appearance: none;
    pointer-events: none;
    position: absolute;
  }

  [arc-radio] {
    flex: 1;
    display: flex;
    justify-content: center;
    border-radius: var(--arc-computed-rbo-border-radius);
    outline: var(--arc-internal-rbo-outline, none);
    outline-offset: var(--arc-computed-rbo-border-width);
  }

  [arc-label] {
    flex: 1;
    line-height: var(--arc-computed-rbo-line-height);
    overflow: hidden;
  }

  [arc-label-content] {
    flex: 1;
    display: flex;
    align-items: safe center;
    justify-content: center;

    height: var(--arc-computed-rbo-size);
    min-width: var(--arc-computed-rbo-size);

    border-radius: var(--arc-computed-rbo-border-radius);
    border: var(--arc-computed-rbo-border-width) solid var(--arc-internal-rbo-border-color);
    background: var(--arc-internal-rbo-background);
    color: var(--arc-internal-rbo-color);
    font-weight: var(--arc-internal-rbo-font-weight, unset);

    transition-property: border, background, color, font-weight;
    transition-duration: 200ms;
    transition-timing-function: ease-out;

    &:has([data-element=text]) {
      padding: 0 var(--arc-computed-rbo-text-padding);
    }

    &:not(:has([data-element=text])) {
      padding: 0 var(--arc-computed-rbo-icon-padding);
    }
  }

  [arc-icon-text] [data-element] {
    min-width: min-content;
    overflow: hidden;
    overflow-wrap: break-word;
  }
}

}
`,dn=h(P,["hug","fill"]),pn=h(_,["lg","md"]),vn=({children:a,label:r,icon:e,...t})=>{const n=a||(0,o.jsx)(pr,{icon:e,text:r});return(0,o.jsx)("li",{"arc-radio-bar-option":"","data-is-checked":t?.checked||void 0,children:(0,o.jsx)(Ga,{...t,label:n,layout:"fill",useMask:!1})})},hn=({children:a,size:r="lg",layout:e="hug",...t})=>(0,o.jsx)("ul",{...t,"arc-radio-bar":"","data-size":r,"data-layout":e,role:"radiogroup",children:a});x("ArcRadioBar",sn);const un=h(_,["inherit","md","sm"]),E={On:"on",Off:"off"},bn=m`
@layer arc-components {

[arc-switch] {
  --arc-internal-switch-background: var(--arc-color-alpha-white-100);
  --arc-internal-switch-shadow: var(--arc-shadow-md);

  --arc-internal-inputmask-border-radius: var(--arc-border-radius-pill);

  --arc-label-align-items: stretch; /* ArcLabel • this allows the mask to
                                      stay inline with the label text
                                      regardless of applied line-height */


  /* COLORS via STATES ---------------------------------------------• */

  &:has(:disabled) {
    --arc-internal-switch-background: var(--arc-color-alpha-white-70);
    --arc-internal-switch-shadow: 0 0 1em max(1px, 0.0625em) var(--arc-color-alpha-white-50) inset;

    [arc-switch-label] {
      --arc-label-cursor: not-allowed;
      --arc-label-opacity: var(--arc-alpha-60);
    }
  }

  &[data-state=off] {
    --arc-internal-inputmask-background: var(--arc-color-alpha-black-05);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-30);

    &:not(:has(:disabled)):hover {
      --arc-internal-inputmask-background: var(--arc-color-alpha-black-10);
      --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);
    }

    &:has(:disabled) {
    --arc-internal-inputmask-background: var(--arc-color-alpha-black-05);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-20);
    }
  }

  &[data-state=on] {
    --arc-internal-inputmask-background: var(--arc-color-alpha-black-100);
    --arc-internal-inputmask-border-color: var(--arc-color-alpha-black-100);

    &:not(:has(:disabled)):hover {
      --arc-internal-inputmask-opacity: var(--arc-alpha-60);
    }

    &:has(:disabled) {
      --arc-internal-inputmask-opacity: var(--arc-alpha-30);
    }
  }


  /* SIZES  --------------------------------------------------------• */

  &[data-size=inherit] {
    --arc-inherit-switch-font-size: 1em;
    --arc-inherit-switch-gap: 0.5em;  /* 8px @ 100%/16px base */
    --arc-inherit-switch-width: 2.5em; /* 40px @ 100% */
    --arc-inherit-switch-height: 1.5em; /* 24px @ 100% */
    --arc-inherit-switch-x-shift: 0.25em; /* 4px @ 100% */
    --arc-inherit-switch-size: 1.5em; /* 24px @ 100% */
    --arc-inherit-switch-gap: 0.5em; /* 8px @ 100% */
  }

  &[data-size=md] { /* @default */
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-internal-switch-width: 40;
    --arc-internal-switch-height: 24;
    --arc-internal-switch-x-shift: 4;
    --arc-internal-switch-size: 24;
    --arc-internal-switch-gap: 8;
  }

  &[data-size=sm] {
    --arc-modifier: 12; /* unitless, desired font size at 100% */
    --arc-internal-switch-width: 30;
    --arc-internal-switch-height: 14;
    --arc-internal-switch-x-shift: 1;
    --arc-internal-switch-size: 16;
    --arc-internal-switch-gap: 8;
  }


  /* COMPUTED ------------------------------------------------------• */

  & {
    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;

    --arc-computed-switch-font-size: var(--arc-inherit-switch-font-size, calc(var(--arc-modifier-px)));
    --arc-computed-switch-gap: var(--arc-inherit-switch-gap, calc(var(--arc-internal-switch-gap) / var(--arc-modifier-em)));
    --arc-computed-switch-width: var(--arc-inherit-switch-width, calc(var(--arc-internal-switch-width) / var(--arc-modifier-em)));
    --arc-computed-switch-height: var(--arc-inherit-switch-height, calc(var(--arc-internal-switch-height) / var(--arc-modifier-em)));
    --arc-computed-switch-x-shift: var(--arc-inherit-switch-x-shift, calc(var(--arc-internal-switch-x-shift) / var(--arc-modifier-em)));
    --arc-computed-switch-size: var(--arc-inherit-switch-size, calc(var(--arc-internal-switch-size) / var(--arc-modifier-em)));


    /* Intern-Component Communication -------------------------------• to ArcBooleanInputMask */
    --arc-internal-inputmask-width: var(--arc-computed-switch-width);
    --arc-internal-inputmask-height: var(--arc-computed-switch-height);
    --arc-internal-inputmask-x-shift: var(--arc-computed-switch-x-shift);
    --arc-internal-inputmask-size: var(--arc-computed-switch-size);
  }


  /* SWITCH PLACEMENT ----------------------------------------------• */

  &[data-state=off] {
    --arc-internal-switch-mask-translate-x: calc(-100% + var(--arc-computed-switch-x-shift));
  }

  &[data-state=on] {
    --arc-internal-switch-mask-translate-x: calc(0% - var(--arc-computed-switch-x-shift));
  }
}


/* CORE STYLES -----------------------------------------------------• */

[arc-switch] {
  font-size: var(--arc-switch-font-size, var(--arc-computed-switch-font-size));
  display: inline-flex;
  align-items: baseline;
  gap: var(--arc-computed-switch-gap);

  [arc-switch-label] {
    font-size: inherit;
    hyphens: auto;
  }

  [arc-input-type=switch] {
    &:where(:not(:checked)) {
      --arc-internal-input-z-index: 1;
    }
  }

  [arc-input-mask-wrapper] {
    align-self: flex-start;
    height: 1lh;
    display: inline-flex;
    align-items: center;
    justify-content: center;
  }

  [data-element=mask] {
  }

  [data-element=mask] {
    position: absolute;
    top: 50%;
    left: 50%;
    translate: var(--arc-internal-switch-mask-translate-x) -50%;

    display: inline-flex;
    width: var(--arc-computed-switch-size);
    height: var(--arc-computed-switch-size);

    transition-property: opacity, background, color, border-color, outline, translate;
    transition-duration: 100ms;
    transition-timing-function: ease-in-out;
    transition-behavior: allow-discrete;
    transition-behavior: allow-discrete;

    background: var(--arc-internal-switch-background);
    box-shadow: var(--arc-internal-switch-shadow);
    border: var(--arc-internal-inputmask-border);
    border-radius: var(--arc-border-radius-circle);
    pointer-events: none;
  }
}

}
`,mn=({disabled:a,id:r,name:e,size:t="md",value:n,labelOff:i,labelOn:c,onChange:s,...l})=>{const d=(0,g.useMemo)(()=>e||N("arc-switch-name"),[e]),p=(0,g.useMemo)(()=>r&&`${r}-off`||N("arc-switch-off"),[r]),v=(0,g.useMemo)(()=>r&&`${r}-on`||N("arc-switch-on"),[r]),u=A=>{if(A.code==="Space"){A.preventDefault(),A.stopPropagation();const y=n===E.On?E.Off:E.On;s?.({value:y},A)}},f=A=>{const y={on:dr(c),off:dr(i)};return y?.on&&y?.off?y[A]:!y?.on&&y?.off?`${y.off}: ${A}`:y?.on&&!y?.off?`${y.on}: ${A}`:A},k=(0,o.jsxs)(Yr,{type:"switch",onKeyDown:u,children:[(0,o.jsx)(wr,{type:"switch",name:d,id:p,value:E.Off,checked:n===E.Off,disabled:a,"aria-label":f(E.Off)?.trim(),onChange:s}),(0,o.jsx)(wr,{type:"switch",name:d,id:v,value:E.On,checked:n===E.On,disabled:a,"aria-label":f(E.On)?.trim(),onChange:s}),(0,o.jsx)("span",{"data-element":"mask"})]}),C=!c&&n===E.Off?v:p,S=!i&&n===E.On?p:v,O=!!(c||i)&&(0,o.jsxs)(o.Fragment,{children:[!!i&&(0,o.jsx)(Q,{htmlFor:C,"arc-switch-label":c?E.Off:"",children:(0,o.jsx)(rr,{children:i})}),k,!!c&&(0,o.jsx)(Q,{htmlFor:S,"arc-switch-label":i?E.On:"",children:(0,o.jsx)(rr,{children:c})})]});return(0,o.jsx)("span",{...l,"arc-switch":"","data-state":n,"data-size":t,role:"radiogroup",children:O||k})};x("ArcSwitch",bn);const gn=["title","heading","subheading","body"],Ja=["h1","h2","h3","h4","h5","h6"],Qa=["code","div","li","p","pre","span"],re=[...Ja,...Qa],fn=["title-inherit","title-lg","title-md","title-sm"],xn=["heading-inherit","heading-lg","heading-md","heading-sm"],yn=["subheading-inherit","subheading-lg","subheading-md","subheading-sm"],wn=h($,["inherit","primary","primary-inverse"]),kn=["body-inherit","body-lg","body-md","body-sm","body-xs"],An=h($,["inherit","primary","secondary","primary-inverse","secondary-inverse","critical","warning","success","info"]),zn=h(Jr,["inherit","start","center","justify","end"]),Cn=h(Qr,["inherit","loose","standard","tight"]),Sn={h1:"title",h2:"heading",h3:"heading",h4:"subheading",h5:"subheading",h6:"subheading",p:"body",span:"body",div:"body",code:"body",pre:"body",li:"body"},_n=a=>{const{align:r,lineHeight:e,size:t,variant:n,isMonospace:i,tag:c,...s}=a,l=Sn?.[c]??"body",d=t?.includes("inherit")?t?.split("-")?.[0]:t;return{...s,"arc-text":d||l,"data-is-monospace":i?"":void 0,"data-align":r?.includes("inherit")?void 0:r,"data-variant":n?.includes("inherit")?void 0:n,"data-line-height":e?.includes("inherit")?void 0:e}},En=m`
@layer arc-components {

[arc-text] {

  /* LINE HEIGHT ---------------------------------------------------• */

  &[data-line-height=standard] {
    --arc-text-line-height: var(--arc-line-height-standard);
  }

  &[data-line-height=loose] {
    --arc-text-line-height: var(--arc-line-height-loose);
  }

  &[data-line-height=tight] {
    --arc-text-line-height: var(--arc-line-height-tight);
  }

  /* ALIGNMENT -----------------------------------------------------• */

  &[data-align=start] {
    --arc-text-align: start;
  }

  &[data-align=center] {
    --arc-text-align: center;
  }

  &[data-align=justify] {
    --arc-text-align: justify;
  }

  &[data-align=end] {
    --arc-text-align: end;
  }

  /* VARIANTS ------------------------------------------------------• */

  &[data-variant=primary] {
    --arc-text-color: var(--arc-color-text-primary);
  }

  &[data-variant=primary-inverse] {
    --arc-text-color: var(--arc-color-text-inverse);
  }

  &[data-variant=secondary] {
    --arc-text-color: var(--arc-color-text-secondary);
  }

  &[data-variant=secondary-inverse] {
    --arc-text-color: var(--arc-color-alpha-white-60);
  }

  &[data-variant=critical] {
    --arc-text-color: var(--arc-color-text-critical);
  }

  &[data-variant=warning] {
    --arc-text-color: var(--arc-color-text-warning);
  }

  &[data-variant=success] {
    --arc-text-color: var(--arc-color-text-success);
  }

  &[data-variant=info] {
    --arc-text-color: var(--arc-color-text-info);
  }
}

/* FONT PROPERTIES -------------------------------------------------• */

[arc-text^=title] {
  --arc-text-font-family: var(--arc-font-family-title);
  --arc-text-font-weight: var(--arc-font-weight-title);
  --arc-text-letter-spacing: var(--arc-letter-spacing-title);
  --arc-text-line-height: var(--arc-line-height-title);

  --arc-text-font-size: 2em; /* @default title • 32÷16*1em • same as 'title-md' @ 100% */

  &:where([arc-text*=-lg]) {
    --arc-text-font-size: var(--arc-font-size-title-lg);
  }

  &:where([arc-text*=-md]) {
    --arc-text-font-size: var(--arc-font-size-title-md);
  }

  &:where([arc-text*=-sm]) {
    --arc-text-font-size: var(--arc-font-size-title-sm);
  }
}

[arc-text^=heading] {
  --arc-text-font-family: var(--arc-font-family-heading);
  --arc-text-font-weight: var(--arc-font-weight-heading);
  --arc-text-letter-spacing: var(--arc-letter-spacing-heading);
  --arc-text-line-height: var(--arc-line-height-heading);

  --arc-text-font-size: 1.25em; /* @default heading • 20÷16*1em • same as 'heading-md' @ 100% */

  &:where([arc-text*=-lg]) {
    --arc-text-font-size: var(--arc-font-size-heading-lg);
  }

  &:where([arc-text*=-md]) {
    --arc-text-font-size: var(--arc-font-size-heading-md);
  }

  &:where([arc-text*=-sm]) {
    --arc-text-font-size: var(--arc-font-size-heading-sm);
  }
}

[arc-text^=subheading] {
  --arc-text-font-family: var(--arc-font-family-subheading);
  --arc-text-font-weight: var(--arc-font-weight-subheading);
  --arc-text-letter-spacing: unset;
  --arc-text-line-height: var(--arc-line-height-subheading);

  --arc-text-font-size: 0.875em; /* @default subheading • 14÷16*1em • same as 'subheading-md' @ 100% */

  &:where([arc-text*=-lg]) {
    --arc-text-font-size: var(--arc-font-size-subheading-lg);
  }

  &:where([arc-text*=-md]) {
    --arc-text-font-size: var(--arc-font-size-subheading-md);
  }

  &:where([arc-text*=-sm]) {
    --arc-text-font-size: var(--arc-font-size-subheading-sm);
  }
}

[arc-text^=body] {
  --arc-text-font-family: var(--arc-font-family-body);
  --arc-text-font-weight: var(--arc-font-weight-body);
  --arc-text-line-height: var(--arc-line-height-body);
  --arc-text-font-size: 0.875em; /* @default body • 14÷16*1em • same as 'body-md' @ 100% */

  &:where([arc-text*=-lg]) {
    --arc-text-font-size: var(--arc-font-size-body-lg);
  }

  &:where([arc-text*=-md]) {
    --arc-text-font-size: var(--arc-font-size-body-md);
  }

  &:where([arc-text*=-sm]) {
    --arc-text-font-size: var(--arc-font-size-body-sm);
  }

  &:where([arc-text*=-xs]) {
    --arc-text-font-size: var(--arc-font-size-body-xs);
  }
}

[arc-text]:where(pre, code) {
  --arc-text-font-size: 1em;
}

[arc-text]:where(pre, code, [data-is-monospace]) {
  --arc-text-font-family: var(--arc-font-family-mono);
  --arc-text-line-height: var(--arc-line-height-mono);
}


/* CORE STYLES -----------------------------------------------------• */

[arc-text] {
  /* conveniences for element properties */
  flex: var(--arc-text-flex, unset);
  hyphens: var(--arc-text-hyphens, auto); /* not unset as fallback so text-breaks by default */
  opacity: var(--arc-text-opacity, unset);
  text-align: var(--arc-text-align, unset);
  white-space: var(--arc-text-white-space, unset);

  color: var(--arc-text-color, unset);

  font-size: var(--arc-text-font-size, unset);
  font-family: var(--arc-text-font-family, unset);
  font-weight: var(--arc-text-font-weight, unset);
  line-height: var(--arc-text-line-height, unset);
  letter-spacing: var(--arc-text-letter-spacing, unset);

  font-variation-settings: var(--arc-font-variation-settings, var(--arc-internal-font-vs));
}

}
`,jn=a=>(0,g.forwardRef)((r,e)=>(0,g.createElement)(a,{..._n({...r,tag:a}),ref:e})),$n=[...re].reduce((a,r)=>({...a,[r]:jn(r)}),{});x("Arc",En);const ar=a=>(0,o.jsx)(o.Fragment,{children:!!a.children&&(0,o.jsx)("span",{...a})}),Tn=a=>(0,o.jsx)(o.Fragment,{children:!!a.children&&(0,o.jsx)("span",{...a})}),Wr=({children:a,...r})=>(0,o.jsx)(o.Fragment,{children:!!a&&(0,o.jsx)(Q,{...r,children:(0,o.jsx)(rr,{children:a})})}),In=({children:a,layout:r,size:e,variant:t,tag:n="span",...i})=>(0,o.jsx)(n,{...i,"data-layout":r,"data-size":e,"data-variant":t,children:a}),Rn=()=>(0,o.jsx)("span",{"input-element":"spacer"}),Ln=a=>(0,o.jsx)(ar,{...a,"input-element":"header"}),Pn=a=>(0,o.jsx)(Wr,{...a,"input-element":"header-label"}),Mn=a=>(0,o.jsx)(ar,{...a,"input-element":"header-slot"}),On=a=>(0,o.jsx)(ar,{...a,"input-element":"body"}),Bn=a=>(0,o.jsx)(Wr,{...a,"input-element":"input-prefix"}),Hn=a=>(0,o.jsx)(Wr,{...a,"input-element":"input-suffix"}),Vn=a=>(0,o.jsx)(Tn,{...a,"input-element":"input-action"}),Nn=a=>(0,o.jsx)(ar,{...a,"input-element":"footer"}),Un=a=>(0,o.jsx)(ar,{...a,"input-element":"footer-caption"}),Fn=a=>(0,o.jsx)(ar,{...a,"input-element":"footer-slot"}),Dn=a=>r=>{if(!(r?.target instanceof HTMLInputElement))return;const e={name:r?.target?.name,value:r?.target?.value};a?.(e,r)},Yn=m`
@layer arc-components {

/* --arc[-internal, -modifier, -computed] • NOT FOR CONSUMER USE ---• */

[arc-input] {

  /* SIZES ---------------------------------------------------------• */

  &[data-size=xl] {
    --arc-internal-input-height: 48;
    --arc-internal-input-padding-inline-end: 8;
  }

  &:is([data-size=lg], [data-size=inherit]) { /* @default • base size at 100% */
    --arc-internal-input-height: 40;
  }

  &[data-size=md] {
    --arc-internal-input-height: 32;
    --arc-internal-input-border-radius: 4;
    --arc-internal-input-action-max-height: 80%;
  }

  & {
    --arc-modifier: 14; /* unitless, desired font size at 100% */
    --arc-modifier-slot: 12; /* unitless, desired font size at 100% */
    --arc-internal-input-gap: 6;
    --arc-internal-input-slot-gap: 4;
    --arc-internal-input-padding-inline: 12;
    --arc-internal-input-padding-inline-end: 3;
    --arc-internal-input-border-radius: 8;
    --arc-internal-input-border-width: 1;
    --arc-internal-input-outline-size: 2;
    --arc-internal-input-outline-offset: -1;

    /* COMPUTED VARIABLES ------------------------------------------• */

    --arc-modifier-px: var(--arc-modifier) * 1px;
    --arc-modifier-em: var(--arc-modifier) * 1em;
    --arc-modifier-slot-px: var(--arc-modifier-slot) * 1px;
    --arc-modifier-slot-em: var(--arc-modifier-slot) * 1em;

    --arc-computed-input-font-size: calc(var(--arc-modifier-px));
    --arc-computed-input-slot-font-size: calc(var(--arc-modifier-slot-px));

    --arc-computed-input-border-radius: calc(var(--arc-internal-input-border-radius) / var(--arc-modifier-em));
    --arc-computed-input-border-width: calc(var(--arc-internal-input-border-width) / var(--arc-modifier-em)); /* 1px at 100% */
    --arc-computed-input-outline-size: calc(var(--arc-internal-input-outline-size) / var(--arc-modifier-em)); /* 2px at 100% */
    --arc-computed-input-outline-offset: calc(var(--arc-internal-input-outline-offset) / var(--arc-modifier-em)); /* -1px at 100% */

    --arc-computed-input-height: calc(var(--arc-internal-input-height) / var(--arc-modifier-em));
    --arc-computed-input-gap: calc(var(--arc-internal-input-gap) / var(--arc-modifier-em));
    --arc-computed-input-padding-inline: calc(var(--arc-internal-input-padding-inline) / var(--arc-modifier-em));
    --arc-computed-input-padding-inline-end: calc(var(--arc-internal-input-padding-inline-end) / var(--arc-modifier-em));
    --arc-computed-input-slot-gap: calc(var(--arc-internal-input-slot-gap) / var(--arc-modifier-slot-em));


    /* Inter-Component Communication -------------------------------• */

    --arc-label-cursor: var(--arc-internal-input-cursor, inherit); /* ArcLabel • cursor syncing */
    --arc-icon-text-modifier: var(--arc-modifier-slot); /* ArcIconText • modifier for font-size */

    &[data-size=inherit] {
      --arc-computed-input-font-size: 0.875em; /* 14px at 100% */
      --arc-computed-input-slot-font-size: 0.75em; /* 12px at 100% */
    }

    &[data-size=md] {
      /* Inter-Component Communication -------------------------------• */
      --arc-button-border-radius: var(--arc-border-radius-sm);
      --arc-button-outline-offset: 0;
      --arc-button-font-size: 0.8em; /* ArcButton • the input size 'md' needs
                                        a slightly smaller "input-action" button
                                        footprint; there's no direct access to
                                        ArcButton['size'] prop in this context */
    }
  }

  /* VARIANTS ------------------------------------------------------• */

  & {
    --arc-internal-input-color: var(--arc-color-text-primary);
    --arc-internal-input-header-label-color: inherit;
    --arc-internal-input-header-slot-color: var(--arc-color-text-secondary);
    --arc-internal-input-footer-caption-color: var(--arc-color-text-secondary);
    --arc-internal-input-footer-slot-color: var(--arc-color-text-secondary);

    &[data-variant=outline] { /* @default */
      --arc-internal-input-background: var(--arc-color-mono-white);
      --arc-internal-input-border-color: var(--arc-color-border-stark);
      --arc-internal-input-border-color-hover: var(--arc-color-border-full);

      --arc-internal-input-color-disabled: var(--arc-color-text-secondary);
      --arc-internal-input-background-disabled: var(--arc-color-background-secondary);
      --arc-internal-input-border-color-disabled: var(--arc-color-border-standard);

      --arc-internal-input-color-readonly: var(--arc-color-text-primary);
      --arc-internal-input-background-readonly: var(--arc-color-background-secondary);
      --arc-internal-input-border-color-readonly: var(--arc-color-border-standard);

      --arc-internal-input-color-invalid: var(--arc-color-text-critical);
      --arc-internal-input-background-invalid: var(--arc-color-mono-white);
      --arc-internal-input-border-color-invalid: var(--arc-color-text-critical);
    }

    &[data-variant=solid] {
      --arc-internal-input-background: var(--arc-color-alpha-black-03);
      --arc-internal-input-border-color: transparent;
      --arc-internal-input-border-color-hover: var(--arc-color-border-stark);

      --arc-internal-input-color-invalid: var(--arc-color-text-critical);
      --arc-internal-input-background-invalid: var(--arc-color-critical-light);
      --arc-internal-input-border-color-invalid: transparent;

      --arc-internal-input-color-disabled: var(--arc-color-text-secondary);
      --arc-internal-input-background-disabled: var(--arc-color-alpha-black-05);
      --arc-internal-input-border-color-disabled: transparent;

      --arc-internal-input-color-readonly: var(--arc-color-text-primary);
      --arc-internal-input-background-readonly: var(--arc-color-alpha-black-05);
      --arc-internal-input-border-color-readonly: transparent;
    }
  }


  /* STATES --------------------------------------------------------• */

  &:not(:has(input:read-only, input:disabled)) {
    &:has([input-element=header-label]:hover, [input-element=body]:hover) {
      --arc-internal-input-border-color: var(--arc-input-border-color-hover, var(--arc-internal-input-border-color-hover));
    }

    &:has([input-element=body]:where(:focus-within, :has(:focus, :active))) {
      --arc-internal-input-outline-color: var(--arc-color-border-focus);
    }
  }

  &:has(input:read-only) {
    --arc-internal-input-cursor: default;
    --arc-internal-input-color: var(--arc-internal-input-color-readonly);
    --arc-internal-input-background: var(--arc-internal-input-background-readonly);
    --arc-internal-input-border-color: var(--arc-internal-input-border-color-readonly);
  }

  &:has(input:disabled) {
    --arc-internal-input-cursor: not-allowed;
    --arc-internal-input-color: var(--arc-internal-input-color-disabled);
    --arc-internal-input-background: var(--arc-internal-input-background-disabled);
    --arc-internal-input-border-color: var(--arc-internal-input-border-color-disabled);
  }

  &:has(input:user-invalid:not(:focus)) {
    --arc-internal-input-color: var(--arc-internal-input-color-invalid);
    --arc-internal-input-background: var(--arc-internal-input-background-invalid);
    --arc-internal-input-border-color: var(--arc-internal-input-border-color-invalid);
    --arc-internal-input-footer-caption-color: var(--arc-internal-input-color-invalid);
  }


  /* LAYOUTS -------------------------------------------------------• */

  &[data-layout=fill] { /* @default */
    --arc-internal-input-display: flex;
  }

  &[data-layout=hug] {
    --arc-internal-input-display: inline-flex;
  }

  & {
    --arc-internal-input-border: var(--arc-border-width-sm) solid var(--arc-internal-input-border-color);
    --arc-internal-input-outline: var(--arc-computed-input-outline-size) solid var(--arc-internal-input-outline-color, transparent);
  }
}

[arc-input]:not([hidden]) {
  display: var(--arc-internal-input-display, flex);
  flex-direction: var(--arc-internal-input-flex-direction, column);
  gap: var(--arc-computed-input-slot-gap);

  :is([input-element=header], [input-element=footer]) {
    display: flex;
    justify-content: space-between;
    font-size: var(--arc-input-slot-font-size, var(--arc-computed-input-slot-font-size));
    gap: var(--arc-computed-input-slot-gap);
  }

  [input-element=spacer] {
    display: safe flex-end;
  }

  [input-element=header] {
    align-items: safe flex-end;
  }

  [input-element=header-label] {
    flex: 1;
    color: var(--arc-input-header-label-color, var(--arc-internal-input-header-label-color));
    font-weight: var(--arc-input-header-label-font-weight, var(--arc-font-weight-600));
  }

  [input-element=header-slot] {
    color: var(--arc-input-header-slot-color, var(--arc-internal-input-header-slot-color));
    min-width: var(--arc-input-header-slot-min-width, fit-content);
  }

  [input-element=footer] {
    align-items: baseline;
  }

  [input-element=footer-caption] {
    flex: 1;
    color: var(--arc-input-footer-caption-color, var(--arc-internal-input-footer-caption-color));
  }

  [input-element=footer-slot] {
    color: var(--arc-input-footer-slot-color, var(--arc-internal-input-footer-slot-color));
    min-width: var(--arc-input-footer-slot-min-width, fit-content);
  }

  [input-element=body] {
    flex: none;
    display: flex;
    align-items: center;
    gap: var(--arc-computed-input-gap);
    height: var(--arc-computed-input-height);
    font-size: var(--arc-computed-input-font-size);

    color: var(--arc-input-color, var(--arc-internal-input-color));
    background: var(--arc-input-background, var(--arc-internal-input-background));
    border: var(--arc-input-border, var(--arc-internal-input-border));
    border-radius: var(--arc-input-border-radius, var(--arc-computed-input-border-radius));
    outline: var(--arc-internal-input-outline, none);
    outline-offset: var(--arc-computed-input-outline-offset);

    transition-property: border, background, color, height;
    transition-duration: 200ms;
    transition-timing-function: ease-out;

    cursor: var(--arc-internal-input-cursor, unset);

    padding-inline: var(--arc-computed-input-padding-inline);

    &:has([input-element=input-action]) {
      padding-inline-end: var(--arc-computed-input-padding-inline-end);
    }

    input {
      cursor: inherit;
      apprearance: none;
      -webkit-apprearance: none;
      align-self: stretch;
      flex: 1;
      font: inherit;
      border: var(--arc-border-width-sm) solid transparent;
      background: transparent;
      padding: 0;
      margin: 0;
      outline: none;
      line-height: normal;
      min-width: 1em;
      max-width: 100%;
      text-align: var(--arc-input-text-align, unset);
    }
  }

  [input-element=input-prefix] {
    flex: none;
  }

  [input-element=input-suffix] {
    flex: none;
    opacity: var(--arc-input-suffix-opacity, var(--arc-alpha-40));
  }

  [input-element=input] {
    flex: 1;
    display: flex;
  }

  [input-element=input-action] {
    flex: none;
    cursor: inherit;
    align-self: stretch;
    display: flex;
    align-items: center;
    justify-content: center;

    button {
      max-height: var(--arc-internal-input-action-max-height, unset);
    }
  }
}

}
`,Zn={onChangeHandler:Dn,Wrapper:In,Spacer:Rn,Header:Ln,HeaderLabel:Pn,HeaderSlot:Mn,InputBody:On,InputPrefix:Bn,InputSuffix:Hn,InputAction:Vn,Footer:Nn,FooterCaption:Un,FooterSlot:Fn};x("ArcInput",Yn);const j=Zn,Xn=h(P,["fill","hug"]),qn=h(_,["inherit","xl","lg","md"]),Wn=h($,["outline","solid"]),Kn=Xn,Gn=qn,Jn=Wn,Qn=["text","number","password","email","url"],ro=(0,g.forwardRef)(({layout:a="hug",size:r="inherit",variant:e="outline",inputProps:t,headerLabel:n,headerSlot:i,inputPrefix:c,inputAction:s,inputSuffix:l,footerCaption:d,footerSlot:p,...v},u)=>{const{id:f,onChange:k,...C}=t,S=C?.type||"text",O=C?.disabled||void 0,A=C?.readOnly||void 0,y=C?.required||void 0,I=!!(n||i),B=!!(d||p);return(0,o.jsxs)(j.Wrapper,{...v,"arc-text-input":"","arc-input":S,layout:a,size:r,variant:e,children:[I&&(0,o.jsxs)(j.Header,{children:[(0,o.jsx)(j.HeaderLabel,{htmlFor:f,children:n}),!!i&&(0,o.jsx)(j.Spacer,{}),(0,o.jsx)(j.HeaderSlot,{children:i})]}),(0,o.jsxs)(j.InputBody,{children:[(0,o.jsx)(j.InputPrefix,{htmlFor:f,children:c}),(0,o.jsx)("input",{...C,ref:u,id:f,"arc-input-type":S,onChange:j.onChangeHandler(k),"aria-disabled":O,"aria-readonly":A,"aria-required":y}),(0,o.jsx)(j.InputSuffix,{htmlFor:f,children:l}),(0,o.jsx)(j.InputAction,{children:s})]}),B&&(0,o.jsxs)(j.Footer,{children:[(0,o.jsx)(j.FooterCaption,{children:d}),!!p&&(0,o.jsx)(j.Spacer,{}),(0,o.jsx)(j.FooterSlot,{children:p})]})]})}),ao=m`
@layer arc-components {

  [arc-toast] {
    --arc-toast-padding: 0.75em 0.75em 0.75em 1em;
    --arc-toast-transition-duration: 300ms;
    --arc-toast-offset: 1em;

    --arc-modifier: 14;
    --arc-toast-content-gap: max(0.125em, calc(2 / var(--arc-modifier) * 1em));

    /* SIZES ----------------------------------------------------------------• */

    &[data-size=md] { /* @default */
      --arc-toast-font-size: var(--arc-font-size-body-lg);
      --arc-toast-content-font-size: var(--arc-font-size-body-md); /* 14px @ 100% */
    }

    &[data-size=inherit] {
      --arc-toast-font-size: 1em;
      --arc-toast-content-font-size: 0.875em;
    }
    
    /* VARIANTS -------------------------------------------------------------• */

    &[data-variant=common] { /* @default */
      --arc-toast-background: var(--arc-color-text-primary);
      --arc-toast-color: var(--arc-color-text-inverse);
    }
  }

  /* CORE STYLES --------------------------------------------------• */

  [arc-toast] {
    inset: var(--arc-toast-inset, 
      var(--top, auto) 
      var(--right, auto) 
      var(--bottom, auto) 
      var(--left, auto)
    );

    &[data-placement*=top-] {
      --top: var(--arc-toast-offset);
    }

    &[data-placement*=bottom-] {
      --bottom: var(--arc-toast-offset);
    }

    &[data-placement=center] {
      --arc-toast-inset: 0;
    }

    &[data-placement*=-left] {
      --left: var(--arc-toast-offset);
    }

    &[data-placement*=-right] {
      --right: var(--arc-toast-offset);
    }

    &[data-placement*=-center] {
      --left: 50%;
      translate: -50% 0;
    }
  }

  [arc-toast] {
    font-size: var(--arc-toast-font-size);
    content-visibility: hidden;
    align-items: center;
    color: var(--arc-toast-color);
    background-color: var(--arc-toast-background);
    border-radius: var(--arc-toast-border-radius, 0.5em);
    padding: var(--arc-toast-padding);
    gap: 1em;
    box-shadow: var(--arc-shadow-dark-lg);
    min-width: var(--arc-toast-min-width, 25em);
    min-height: var(--arc-toast-min-height, 3.5em);
    border: none;

    &:has([arc-toast-heading]):has([arc-toast-subtext]) {
      --arc-toast-min-height: 5em;
      --arc-toast-content-display: flex;
    }
    
    [arc-toast-body] {
      flex: 1;
      display: flex;
      align-items: baseline;
      gap: 0.5em;
      padding: 0.25em 0; /* vertical padding to align with action button text */
    }

    [arc-toast-icon] {
      height: 1lh;
      padding: 0 0.125em;
    }
    
    [arc-toast-content] {
      line-height: var(--arc-line-height-loose);
      flex: 1;
      font-size: var(--arc-toast-content-font-size);
      hyphens: auto;
      display: var(--arc-toast-content-display, block);
      flex-direction: column;
      gap: var(--arc-toast-content-gap);

      p {
        margin: 0;
      }
    }

    [arc-toast-actions] {
      display: flex;
      gap: 0.5em;
      align-items: center;
      align-self: baseline;
    }
  }
  
  /* POPOVER + state ---------------------------------------------• */

  [arc-toast] {
    opacity: 0;
    transition:
      display var(--arc-toast-transition-duration) allow-discrete,
      overlay var(--arc-toast-transition-duration) allow-discrete,
      opacity var(--arc-toast-transition-duration);
  }

  [arc-toast]:popover-open {
    display: flex;
    opacity: 1;
    content-visibility: visible;
  }

  @starting-style {
    [arc-toast]:popover-open {
      opacity: 0;
    }
  }
}
`,eo=h(_,["inherit","md"]),to=h($,["common"]),no=h(Tr,["","auto","manual"]),oo=["top-left","top-center","top-right","center","bottom-left","bottom-center","bottom-right"],io=(a,r="toggle")=>{const e=document.getElementById(a);({toggle:()=>e?.togglePopover(),show:()=>e?.showPopover(),hide:()=>e?.hidePopover()})[r]()},ae=({id:a,children:r,icon:e,heading:t,subtext:n,dismissProps:i,showDismiss:c=!0,onClose:s,actionSlot:l,size:d="md",variant:p="common",placement:v="bottom-left",getToastId:u,...f})=>{const k=()=>{s?.()},C=!!l||c,S=(0,g.useMemo)(()=>a||N("arc-toast"),[a]);return(0,g.useEffect)(()=>{u?.(S)},[u,S]),(0,o.jsxs)("div",{...f,"arc-toast":"","data-size":d,"data-variant":p,"data-placement":v,id:S,children:[(0,o.jsxs)("div",{"arc-toast-body":"",children:[e&&(0,o.jsx)(w,{icon:e,"arc-toast-icon":""}),(0,o.jsx)("div",{"arc-toast-content":"",children:r||(0,o.jsxs)(o.Fragment,{children:[t&&(0,o.jsx)("header",{"arc-toast-heading":"",children:(0,o.jsx)("strong",{children:t})}),n&&(0,o.jsx)("p",{"arc-toast-subtext":"",children:n})]})})]}),C&&(0,o.jsxs)("div",{"arc-toast-actions":"",children:[l,c&&(0,o.jsx)(W,{icon:"fa-regular fa-xmark-large",layout:"icon",variant:"tertiary-inverse",size:"md",shape:"square","aria-label":"Dismiss",...i,popovertarget:S,onClick:k})]})]})};ae.handleToast=io,x("ArcToast",ao);const co=(a,r=1/0,e=" ")=>[...a].flat(r)?.filter(Boolean)?.join(e)}}]);
