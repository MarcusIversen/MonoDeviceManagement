import { Injectable } from '@angular/core';
import axios from "axios";
import * as http from "http";
import {User} from "../../Models/Interfaces/user";

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
  getRoleUsers: any[] = [];

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

  async updatePassword(id: any, dto:any){
    const httpResult = await customAxios.put('auth/update-password/'+id, dto)
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

  async sendMail(dto: any) {
    const httpResult = await customAxios.post('/User/sendEmail', dto)
    return httpResult.data;
  }

  async createUserAsAdmin(dto: { firstName: string; lastName: string; role: string; password: string; workNumber: string; email: string }) {
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }
}
