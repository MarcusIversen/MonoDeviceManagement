import { Injectable } from '@angular/core';
import axios from "axios";
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
export class UserService {
  firstName: any;
  lastName: any;
  role: any;
  constructor() { }

  async login(dto: any){
    const httpResult = await customAxios.post('auth/login', dto);
    return httpResult.data;
  }

  async register(dto: any){
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }

  async getUserByEmail(dto: any){
    const httpResult = await customAxios.get('/User/email/'+dto)
    return httpResult.data;
  }

  async updateUser(id: any, dto:any){
    const httpResult = await customAxios.put('/User/update/'+id, dto)
    return httpResult.data;
  }

}
