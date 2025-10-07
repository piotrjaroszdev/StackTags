import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Tag } from "../models/Tag";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class TagService {
  private apiUrl = `${environment.apiUrl}/tags`;

  constructor(private http: HttpClient) {}

  getTags(page: number, pageSize: number, sortBy: string, order: string): Observable<Tag[]> {
    return this.http.get<Tag[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&order=${order}`);
  }

  syncTags(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/sync`, {});
  }
}
