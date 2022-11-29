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
export class UserService {

  constructor() { }

  async login(dto: any){
    const httpResult = await customAxios.post('auth/login', dto);
    return httpResult.data;
  }

  async register(dto: any){
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }
  
}