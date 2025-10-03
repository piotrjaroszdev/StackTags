import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Tag } from "../model/Tag";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class TagService {
  private apiUrl = 'http://localhost:5043/api/tags';

  constructor(private http: HttpClient) {}

  getTags(page: number, pageSize: number, sortBy: string, order: string): Observable<Tag[]> {
    return this.http.get<Tag[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}&sortBy=${sortBy}&order=${order}`);
  }

  syncTags(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/sync`, {});
  }
}
