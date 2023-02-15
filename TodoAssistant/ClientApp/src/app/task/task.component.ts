import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { getEnumValues } from '../helpers/enum-helper';
import { validateFormGenerally } from '../helpers/validation-helper';
import { Status } from '../models/status';
import { statusEnumToString, TaskDto } from '../models/task-dto';
import { ErrorService } from '../services/error.service';
import { TasksService } from '../services/tasks.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.scss'],
})
export class TaskComponent {
  private validationMessages = {
    name: {
      required: 'Name field is required',
    },
    priority: {
      required: 'Priority field is required',
    },
    status: {
      required: 'Status field is required',
    },
  };

  taskDto!: TaskDto;
  taskId!: string;
  form!: FormGroup;

  statuses = getEnumValues(Status);
  statusEnumToString = statusEnumToString;

  constructor(
    private _tasksService: TasksService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _toastrService: ToastrService,
    private _errorService: ErrorService
  ) {}

  ngOnInit() {
    this.taskId = this._activatedRoute.snapshot.params['taskId'];

    if (this.isEditMode()) {
      this.loadData();
    } else {
      this.taskDto = <TaskDto>{};
    }

    this.buildForm();
  }

  save() {
    if (!this.validateForm()) return;

    if (this.isEditMode()) {
      this._tasksService.edit(this.taskDto).subscribe(
        (_) => this.success(),
        (err) => this._errorService.handleException(err)
      );
    } else {
      this._tasksService.create(this.taskDto).subscribe(
        (_) => this.success(),
        (err) => this._errorService.handleException(err)
      );
    }
  }

  private loadData() {
    this._tasksService.get(this.taskId).subscribe(
      (task) => {
        this.taskDto = task;

        if (!this.taskDto) {
          this._toastrService.error('Task not found');
        }
      },
      (err) => this._errorService.handleException(err)
    );
  }

  private isEditMode() {
    return !!this.taskId;
  }
  private buildForm() {
    const formControls: any = {
      name: new FormControl(this.taskDto?.name, [Validators.required]),
      priority: new FormControl(this.taskDto?.priority, [Validators.required]),
      status: new FormControl(this.taskDto?.status, [Validators.required]),
    };

    this.form = new FormGroup(formControls);
  }

  private validateForm(): boolean {
    let res = false;

    if (
      !validateFormGenerally(
        this.form,
        this.validationMessages,
        (message: string | undefined) => {
          this._toastrService.warning(message);
        }
      )
    ) {
      return res;
    }

    res = true;
    return res;
  }

  private success() {
    this._toastrService.success('Saved');
    this.navigateToMain();
  }

  private navigateToMain() {
    this._router.navigate(['/']);
  }
}
