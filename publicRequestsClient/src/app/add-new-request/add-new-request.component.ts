import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-new-request',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-new-request.component.html',
  styleUrl: './add-new-request.component.css'
})
export class AddNewRequestComponent {
  formData = {
    name: '',
    phone: '',
    email: '',
    department: '',
    description: ''
  };

statistics: Array<{ [key: string]: any }> = [];
columns: string[] = [];
statisticsError: string = '';

  constructor(private http: HttpClient) {}

  onSubmit() {
    this.http.post('https://localhost:7082/api/RequestController', this.formData)
      .subscribe(response => {
        console.log('Success', response);
      }, error => {
        console.error('Error', error);
      });
  }

  viewStatistics() {
    this.statisticsError = '';
    this.http.get<any[]>('https://localhost:7082/api/RequestController/summary')
      .subscribe(response => {
        this.statistics = response;
              this.columns = response.length ? Object.keys(response[0]) : [];

      }, error => {
        this.statisticsError = 'שגיאה בקבלת נתוני סטטיסטיקה';
        this.statistics = [];
              this.columns = [];

      });
  }
}