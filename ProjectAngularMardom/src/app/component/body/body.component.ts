import { CommonModule, NgOptimizedImage } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Empleados } from '../../models/modelos';
import { ApiService } from '../../service/api.service';

@Component({
  selector: 'app-body',
  standalone: true,
  imports: [
    CommonModule, NgOptimizedImage
  ],
  templateUrl: './body.component.html',
  styleUrl: './body.component.css'
})
export class BodyComponent implements OnInit{
    
  dataTable: any = [];
  
  constructor(
    private apiService: ApiService,
  ){ }

    ngOnInit(): void {
      this.OnDataTable();
    }

    OnDataTable(){
      this.apiService.getAllEmpleados().subscribe(res=>{
        this.dataTable = res;
        console.log(res);
      })
    }
}
