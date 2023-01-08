import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.model';


@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css']
})
export class EmployeesListComponent implements OnInit{

  employees: Employee[] = [
    {
      id: 'testingGUIDValue',
      name: 'John Doe',
      email: 'john.doe@gmail.com',
      phone: 1234567890,
      salary: 60000,
      department: 'Human Resources'
    },
    {
      id: 'testingGUIDValue2',
      name: 'Diane Joe',
      email: 'Diane.Joe@gmail.com',
      phone: 9876543211,
      salary: 65000,
      department: 'Information Technology'      
    },
    {
      id: 'testingGUIDValue3',
      name: 'Sameer Saini',
      email: 'Sameer.Saini@gmail.com',
      phone: 7776543211,
      salary: 68000,
      department: 'Marketing'      
    }
  ];


  constructor() { }

  ngOnInit(): void {

  }
}
