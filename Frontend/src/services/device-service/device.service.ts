import { Injectable } from '@angular/core';
import axios from "axios";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7234',
  headers: {
    Authorization: `Bearer ${localStorage.getItem('token')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  devices = [];

  constructor() { }

  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    this.devices = httpResponse.data;
    return httpResponse.data;
  }
}
