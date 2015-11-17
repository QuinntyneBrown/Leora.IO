 declare module app.diagnostics {

     export interface IConsoleMessage {
         date?: Date;
         message: string;
     }

     export interface IRequestProfile {
         elapsed: number;
         startTime: number;
     }
     export interface IDiagnosticsManager {
         updateRequestProfile(response:any):void;
         requestProfiles:any[];
         urls: IHistoryInfo[];
         consoleMessages: IConsoleMessage[];
         screenCaptures: IScreenCaptureInfo[];
         stackTrace: any[];
         features:any[];
     }

     export interface IScreenCaptureInfo {
         dateTime: string;
         base64StringEncodedImage: string;
     }

     export interface IHistoryInfo {
         dateTime: number;
         url: string;
         path:string;
         changeSuccess: boolean;
         watchers:number;
     }

     export interface IFeature {
         name: string;
         state: boolean;
     }

     export interface IModernizrService {
         autofocus: boolean;
         fontface: boolean;
         backgroundsize: boolean;
         borderimage: boolean;
         borderradius: boolean;
         boxshadow: boolean;
         flexbox: boolean;
         hsla: boolean;
         multiplebgs: boolean;
         opacity: boolean;
         rgba: boolean;
         textshadow: boolean;
         cssanimations: boolean;
         csscolumns: boolean;
         generatedcontent: boolean;
         cssgradients: boolean;
         cssreflections: boolean;
         csstransforms: boolean;
         csstransforms3d: boolean;
         csstransitions: boolean;
         applicationcache: boolean;
         canvas: boolean;
         canvastext: boolean;
         draganddrop: boolean;
         hashchange: boolean;
         history: boolean;
         audio: boolean;
         video: boolean;
         indexeddb: boolean;
         input: boolean;
         inputtypes: boolean;
         localstorage: boolean;
         postmessage: boolean;
         sessionstorage: boolean;
         websockets: boolean;
         websqldatabase: boolean;
         webworkers: boolean;
         geolocation: boolean;
         inlinesvg: boolean;
         smil: boolean;
         svg: boolean;
         svgclippaths: boolean;
         touch: boolean;
         webgl: boolean;
     }

     export interface IRequestProfiler {

     }

     export interface IPrintStackTrace {
         (options:any):any[];
     }

     export interface IErrorRedirectProvider extends ng.IServiceProvider {

     }

     export interface IErrorRedirect {
         redirectPostError(options:any): void;
     }
 }