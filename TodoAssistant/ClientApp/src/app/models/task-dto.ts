import { Status } from './status';

export interface TaskDto {
  id: string;
  name: string;
  priority: number;
  status: Status;
}

export function statusEnumToString(status: Status) {
  switch (status) {
    case Status.Completed:
      return 'Completed';
    case Status.InProgress:
      return 'In Progress';
    case Status.NotStarted:
      return 'Not Started';
    default:
      return 'Unknown';
  }
}
