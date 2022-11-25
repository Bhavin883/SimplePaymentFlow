import axios from "axios";

export type SiteList = {
    id: number,
    name: string,
    pumps: Pump[]
};

export type Pump = {
    id: number,
    name: String,
    locked: Boolean,
    siteId: number
};

export type ReceiptList = {
    id: string,
    start: Date,
    end: Date,
    pumpId: number
};

export const GetSiteListAsync = async (siteName: string): Promise<SiteList[] | undefined> => {
    if(siteName === undefined)
    siteName='';
    var url = `https://localhost:7258/api/Payment/FindSites?siteName=${siteName}`;
   var response =  await axios.get(url)
      if(response.status === 200){
        return response.data;
      }
        return undefined;
};

export const UnlockPumpAsync = async (pumpId: string): Promise<string> => {
    var url = `https://localhost:7258/api/Payment/UnLockPump?id=${pumpId}`;
    var response =  await axios.post(url)
       if(response.status === 200){
         return response.data;
       }
         return "";
 };

 export const LockPumpAsync = async (pumpId: string): Promise<string> => {
    var url = `https://localhost:7258/api/Payment/LockPump?id=${pumpId}`;
    var response =  await axios.post(url)
       if(response.status === 200){
         return response.data;
       }
         return "";
 };

 export const GetReceiptAsync = async (pumpId: string): Promise<ReceiptList[] | undefined>  => {
    if(pumpId === undefined)
    pumpId='';
    var url = `https://localhost:7258/api/Payment/Receipt?id=${pumpId}`;
    var response =  await axios.get(url)
       if(response.status === 200){
         return response.data;
       }
         return undefined;
 };
