import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskDto } from '../models/task-dto';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  private _api = 'https://localhost:44396/api/tasks';

  constructor(private _http: HttpClient) {}

  get(taskName: string) {
    return this._http.get<TaskDto>(this._api + '/' + taskName);
  }

  getAll() {
    return this._http.get<TaskDto[]>(this._api);
  }

  create(task: TaskDto) {
    return this._http.post(this._api, this.toBody(task));
  }

  edit(task: TaskDto) {
    return this._http.put(this._api, this.toBody(task));
  }

  delete(task: TaskDto) {
    return this._http.delete(this._api + '/' + task.id);
  }

  private toBody(task: TaskDto) {
    return {
      task: {
        id: task.id,
        name: task.name,
        priority: task.priority,
        status: +task.status,
      },
    };
  }
}
