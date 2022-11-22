import { Injectable } from '@angular/core';
import axios from "axios";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7234'
})

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor() { }

  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    httpResponse.data;
  }
}


