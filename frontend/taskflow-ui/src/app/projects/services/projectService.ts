import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Project {
  id: number;
  name: string;
  description: string;
  createdAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class projectService {
  private apiUrl = 'http://localhost:5023/api';

  constructor(private http: HttpClient) {}

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.apiUrl);
  }

  createProject(payload: { name: string; description: string }): Observable<Project> {
    return this.http.post<Project>(this.apiUrl, payload);
  }
}
