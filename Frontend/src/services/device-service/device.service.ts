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

  constructor() { }

  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    return httpResponse.data;
  }

  async createDevice(dto: { serialNumber: string; dateOfIssue: string; deviceName: string; userId: string; dateOfTurnIn: string; status: string }) {
    const httpResult = await customAxios.post('device', dto);
    return httpResult.data;
  }
}
