import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, of } from 'rxjs';
import { Empleados } from '../models/modelos';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private http: HttpClient
  ) { }

  urlVerEmpleados = "https://localhost:44348/api/Empleados";

  getAllEmpleados(): Observable<Empleados | undefined>{
    return this.http.get<Empleados>(this.urlVerEmpleados).pipe(
      catchError((error) => {
        console.log(error)
        return of(undefined)
      })
    );
  }
}
