export class ApiResponse {
  pages: Page[];
  totalhits: number;
}
export interface Page {
  pageid:number;
  title:string;
  snippet:string;
  timestamp:string;
}
