export interface Device{
  id: number;
  
  deviceName: string,
  
  serialNumber: string,
  
  status: string,
  
  user?: string;
  userId?: number;
  
  dateOfIssue?: Date;
  dateOfTurnIn?: Date;
  
  requestValue: string;
  requesterId? : number;
  
  errorSubject?: string;
  errorDescription?: string;
 
}
