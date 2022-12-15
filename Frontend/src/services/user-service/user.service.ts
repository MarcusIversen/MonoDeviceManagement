import { Injectable } from '@angular/core';
import axios from "axios";


export const customAxios = axios.create({
  //baseURL: 'https://localhost:7234',
  baseURL: 'https://monodevicemanagementapi.azurewebsites.net',
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

  /**
   * Method for calling the login method from the Backend API
   * @param dto
   */
  async login(dto: any){
    const httpResult = await customAxios.post('auth/login', dto);
    return httpResult.data;
  }

  /**
   * Method for calling the register method from the Backend API
   * @param dto
   */
  async register(dto: any){
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }


  /**
   * Method for calling the getUserByEmail method from the Backend API
   * @param dto
   */
  async getUserByEmail(dto: any){
    const httpResult = await customAxios.get('/User/email/'+dto)
    return httpResult.data;
  }

  /**
   * Method for calling the updateUser method from the Backend API
   * @param id
   * @param dto
   */
  async updateUser(id: any, dto:any){
    const httpResult = await customAxios.put('/User/update/'+id, dto)
    return httpResult.data;
  }

  /**
   * Method for calling the updatePassword method from the Backend API
   * @param id
   * @param dto
   */
  async updatePassword(id: any, dto:any){
    const httpResult = await customAxios.put('auth/update-password/'+id, dto)
    return httpResult.data;
  }

  /**
   * Method for calling the getUsers method from the Backend API
   */
  async getUsers(){
    const httpResult = await customAxios.get<any>('User');
    return httpResult.data;
  }

  /**
   * Method for calling the getUsersTypeUser method from the Backend API
   */
  async getUsersTypeUser(){
    const httpResult = await customAxios.get<any>("User/RoleType");
    this.getRoleUsers = httpResult.data;
    return httpResult.data;
  }

  /**
   * Method for calling the getUserById method from the Backend API
   * @param id
   */
  async getUserById(id: number){
    const httpResult = await customAxios.get<any>('User/'+`${id}`)
    return httpResult.data;
  }

  /**
   * Method for calling the deleteUser method from the Backend API
   * @param id
   */
  async deleteUser(id: number) {
    const httpResult = await customAxios.delete('/User/'+`${id}`);
    return httpResult.data;
  }

  /**
   * Method for calling the sendMail method from the Backend API
   * @param dto
   */
  async sendMail(dto: any) {
    const httpResult = await customAxios.post('/User/sendEmail', dto)
    return httpResult.data;
  }

  /**
   * Method for calling the createUserAsAdmin method from the Backend API
   * @param dto
   */
  async createUserAsAdmin(dto: { firstName: string; lastName: string; role: string; password: string; workNumber: string; email: string }) {
    const httpResult = await customAxios.post('auth/register', dto);
    return httpResult.data;
  }
}
