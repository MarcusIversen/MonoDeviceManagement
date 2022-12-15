import {Injectable} from '@angular/core';
import axios from "axios";

export const customAxios = axios.create({
  baseURL: 'https://localhost:7234',
  //baseURL: 'https://monodevicemanagementapi.azurewebsites.net',
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
        if (localStorage.getItem('token')) {
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

  /**
   * Method for calling the getDevices backend API.
   */
  async getDevices() {
    const httpResponse = await customAxios.get<any>('device');
    this.devices = httpResponse.data;
    return httpResponse.data;
  }

  /**
   * Method for calling the getDeviceOnUser backend API.
   */
  async getDeviceOnUser(id: number) {
    const httpResponse = await customAxios.get<any>('AssignDev/' + `${id}`);
    this.assignedDevices = httpResponse.data;
    return httpResponse.data;
  }

  /**
   * Method for calling the createDevice backend API.
   */
  async createDevice(dto: { serialNumber: string; requestValue: String; dateOfIssue?: string; deviceName: string; userId: any; dateOfTurnIn?: string; status: string }) {
    const httpResult = await customAxios.post('device', dto);
    return httpResult.data;
  }

  /**
   * Method for calling the deleteDevice backend API.
   */
  async deleteDevice(id: number) {
    const httpResult = await customAxios.delete('/Device/' + `${id}`);
    return httpResult.data;
  }

  /**
   * Method for calling the getDeviceById backend API.
   */
  async getDeviceById(id: number) {
    const httpResult = await customAxios.get<any>('Device/' + `${id}`)
    return httpResult.data;
  }

  /**
   * Method for calling the updateDevice backend API.
   */
  async updateDevice(dto: { serialNumber: any; errorSubject: any; requestValue: any; errorDescription: any; id: any; dateOfIssue: any; deviceName: any; userId: any; dateOfTurnIn: any; status: string }, id){
    const httpResult = await customAxios.put('Device/'+`${id}`, dto)
    return httpResult.data;
  }

  /**
   * Method for calling the getDevicesWithStatusMalfunctioned backend API.
   */
  async getDevicesWithStatusMalfunctioned() {
    const httpResponse = await customAxios.get<any>('Malfunctioned');
    this.devices = httpResponse.data;
    return httpResponse.data;
  }


  /**
   * Method for calling the getIkkeSendtRequestValue backend API.
   */
  async getIkkeSendtRequestValue() {
    const httpResult = await customAxios.get<any>('RequestValue/IkkeSendt')
    return httpResult.data;
  }

  /**
   * Method for calling the getSendtRequestValue backend API.
   */
  async getSendtRequestValue() {
    const httpResult = await customAxios.get<any>('RequestValue/Sendt')
    this.devices = httpResult.data;
    return httpResult.data;
  }


}
