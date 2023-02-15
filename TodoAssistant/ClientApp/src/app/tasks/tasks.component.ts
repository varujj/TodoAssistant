import { Component } from '@angular/core';
import { Status } from '../models/status';
import { statusEnumToString, TaskDto } from '../models/task-dto';
import { ErrorService } from '../services/error.service';
import { TasksService } from '../services/tasks.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
})
export class TasksComponent {
  tasks: TaskDto[] = [];

  statuses = Status;
  statusEnumToString = statusEnumToString;

  constructor(
    private _tasksService: TasksService,
    private _errorService: ErrorService
  ) {}

  ngOnInit() {
    this.loadData();
  }

  onDelete(task: TaskDto) {
    this._tasksService.delete(task).subscribe(
      (_) => this.loadData(),
      (err) => this._errorService.handleException(err)
    );
  }

  private loadData() {
    this._tasksService.getAll().subscribe((tasks) => {
      this.tasks = tasks;
    });
  }
}
