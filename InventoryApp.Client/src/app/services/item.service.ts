import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../models/item';
import { environment } from '../../environments/environment';

/**
 * Service for performing operations with Item objects.
 */
@Injectable({
  providedIn: 'root'
})
export class ItemService {

  /**
   * The URL for Item the controller.
   */
  private _itemControllerUrl: string;

  /**
   * Service constructor.
   */
  constructor(private httpClient: HttpClient) {
    this._itemControllerUrl = `${environment.apiUrl}/Items`;
  }

  /**
   * Gets all of the items from the API.
   * @returns Result from the API.
   */
  public getAll(): Observable<Item[]> {
    const httpResult = this.httpClient.get<Item[]>(this._itemControllerUrl);
    return httpResult;
  }

  /**
   * Posts a new item to the API.
   * @param item Item to post.
   * @returns Result from the API.
   */
  public post(item: Item): Observable<Item> {
    const json = JSON.stringify(item);
    const httpResult = this.httpClient.post<Item>(this._itemControllerUrl, json, { headers: { 'Content-Type': 'application/json'} });
    return httpResult;
  }

  /**
   * Puts an update of an item to the API.
   * @param item Item to put to the API.
   * @returns Result from the API.
   */
  public put(item: Item): Observable<Item> {
    const json = JSON.stringify(item);
    const httpResult = this.httpClient.put<Item>(this._itemControllerUrl, json, { headers: { 'Content-Type': 'application/json' } });
    return httpResult;
  }

  /**
   * Deletes matching item from the database using the API.
   * @param item Item to delete.
   * @returns Result from the API.
   */
  public delete(item: Item): Observable<Object> {
    const url = `${this._itemControllerUrl}/${item.id}`;
    const httpResult = this.httpClient.delete(url);
    return httpResult;
  }
}
