import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Task {
  private apiUrl = 'http://localhost:5023/api';

  constructor(private http: HttpClient) {}

  getTasksByProject(projectId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/by-project/${projectId}`);
  }

  createTask(dto: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, dto);
  }
}
