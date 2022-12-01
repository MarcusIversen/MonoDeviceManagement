import { Injectable } from '@angular/core';
import axios from "axios";
import {FormControl} from "@angular/forms";
import * as http from "http";

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

constructor() { }

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

  async createDevice(dto: { serialNumber: string; dateOfIssue: string; deviceName: string; userId: any; dateOfTurnIn: string; status: string }) {
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
}
