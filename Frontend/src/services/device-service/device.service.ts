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
  assignedDevices: any[] = [];
  constructor() { }

  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    return httpResponse.data;
  }

  async getDeviceOnUser(id: number){
    const httpResponse = await customAxios.get<any>('AssignDev/'+`${id}`)
    this.assignedDevices = httpResponse.data;
    return httpResponse.data;
  }
}
