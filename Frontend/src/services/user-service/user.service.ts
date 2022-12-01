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
  getRoleUsers: any[] = [];


  constructor() { }

  async login(dto: any){
    const httpResult = await customAxios.post('auth/login', dto);
    return httpResult.data;
  }

  async register(dto: any){
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }

  async getUsers(){
    const httpResult = await customAxios.get<any>('User');
    return httpResult.data;
  }

  async getUsersTypeUser(){
    const httpResult = await customAxios.get<any>("User/RoleType");
    this.getRoleUsers = httpResult.data;
    return httpResult.data;
  }

  async getUserById(id: number){
    const httpResult = await customAxios.get<any>('User/'+`${id}`)
    return httpResult.data;
  }

  async deleteUser(id: number) {
    const httpResult = await customAxios.delete('/User/'+`${id}`);
    return httpResult.data;
  }

  async sendMail(dto: { subject: string; body: string; email: any }) {
    const httpResult = await customAxios.post('/User/sendEmail', dto)
    return httpResult.data;
  }
}
