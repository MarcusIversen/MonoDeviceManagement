import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/user-service/user.service";
import jwtDecode from "jwt-decode";

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.scss']
})
export class ProfileInfoComponent implements OnInit{
  profilePicture: any;
  email: string = "";
  firstName: string = "";
  lastName: string = "";
  workNumber: string = "";
  privateNumber: string = "";
  privateMail: string = "";
  id: number | undefined;
  user:any;
  isLoading: boolean | undefined;
  showErrorMessage: boolean | undefined;

  constructor(private userService: UserService) {
  }

  async ngOnInit() {
    let token = localStorage.getItem("token");
    if(token){
      let decodedToken = jwtDecode(token) as Token;
      this.user = await this.userService.getUserByEmail(decodedToken.email);
      this.id = this.user.id;
      this.firstName = this.user.firstName;
      this.lastName = this.user.lastName;
      this.workNumber = this.user.workNumber;
      this.email = this.user.email;
      this.privateMail = this.user.privateMail;
      this.privateNumber = this.user.privateNumber;
      this.profilePicture = this.user.profilePicture;
    }
  }

  selectFile({event}: { event: any }) {
    if(event.target.files){
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload=(event:any)=>{
        this.profilePicture = event.target.result;
      }
    }
  }


  async saveChanges(){
    let dto = {
      id: this.id,
      firstName: this.firstName,
      lastName: this.lastName,
      workNumber: this.workNumber,
      profilePicture: this.profilePicture,
      email: this.email,
      privateNumber: this.privateNumber,
      privateMail: this.privateMail
    }
    this.isLoading = true;

    await this.userService.updateUser(this.id, dto).
      then(()=>
      this.isLoading = false,
    ).catch(error =>{
      this.showErrorMessage = true;
      this.isLoading = false;
      console.log(error)
    })
    window.location.reload();
  }
}

class Token {
  email?: string;
}
