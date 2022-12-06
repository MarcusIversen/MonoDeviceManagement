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
  devices: any[] = [];

constructor() {
  customAxios.interceptors.request.use(
    async config => {
      if(localStorage.getItem('token')) {
        config.headers = {
          'Authorization': `Bearer ${localStorage.getItem('token')}`
        }
      }

      return config;
    },
    error => {
      Promise.reject(error)
    });
}

  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    this.devices = httpResponse.data;
    return httpResponse.data;
  }

  async getDeviceOnUser(id: number){
    const httpResponse = await customAxios.get<any>('AssignDev/'+`${id}`);
    this.assignedDevices = httpResponse.data;
    return httpResponse.data;
  }

  async createDevice(dto: { serialNumber: string; requestValue: String; dateOfIssue: string; deviceName: string; userId: any; dateOfTurnIn: string; status: string }) {
    const httpResult = await customAxios.post('device', dto);
    return httpResult.data;
  }

  async deleteDevice(id: number){
    const httpResult = await customAxios.delete('/Device/'+`${id}`);
    return httpResult.data;
  }

  async getDeviceById(id: number){
    const httpResult = await customAxios.get<any>('Device/'+`${id}`)
    return httpResult.data;
  }

  async updateDevice(dto: { serialNumber: any; id: any; dateOfIssue: any; deviceName: any; userId: any; dateOfTurnIn: any; status: any }, id: number) {
    const httpResult = await customAxios.put('Device/'+`${id}`, dto)
    return httpResult.data;
  }

  async getNotAssignedDevices() {
    const httpResponse = await customAxios.get<any>('NotAssigned/');
    this.devices = httpResponse.data;
    return httpResponse.data;
  }

}
