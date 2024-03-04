import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';

import { IRoom } from '../../shared/models/Room';
import { RoomType } from '../../shared/enums/RoomType';

import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RoomService } from '../../../services/room.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-room-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './room-form.component.html',
  styleUrl: './room-form.component.scss',
})
export class RoomFormComponent implements OnChanges {
  @Input() data: IRoom | null = null;
  @Output() onCloseModal = new EventEmitter();

  roomForm!: FormGroup;
  roomTypes: { id: number; name: string }[] = [];

  constructor(
    private fb: FormBuilder,
    private roomService: RoomService,
    private toastr: ToastrService
  ) {
    this.roomForm = this.fb.group({
      id: [''],
      name: new FormControl('', [Validators.required]),
      code: new FormControl('', [Validators.required]),
      capacity: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required]),
      roomType: new FormControl('', [Validators.required]),
    });

    this.roomTypes = Object.keys(RoomType)
      .filter((key: string) => !isNaN(Number(RoomType[key as keyof typeof RoomType])))
      .map((key: string) => ({ id: RoomType[key as keyof typeof RoomType], name: key }));
  }

  CloseModal() {
    this.roomForm.reset();
    this.onCloseModal.emit(false);
  }

  //kad iskoristis mat dialog, imaces MAT_DIALOG_DATA i u njemu imas ovo, napises u ngOnInit
  ngOnChanges(): void {
    if (this.data) {
      this.roomForm.patchValue({
        id: this.data.id,
        name: this.data.name,
        code: this.data.code,
        capacity: this.data.capacity,
        description: this.data.description,
        roomType: this.data.roomType
      });
    }
  }

  onSubmit() {
    if (this.roomForm.valid) {
      //Katastrofa
      this.roomForm.value.roomType = +this.roomForm.value.roomType
      if (this.data && this.roomForm.value.id) {
        this.roomService
          .updateRoom(this.roomForm.value)
          .subscribe({
            next: (response: any) => {
              this.CloseModal();
              this.toastr.success(response.message);
            },
          });
      } else {
        delete this.roomForm.value.id
        this.roomService.createRoom(this.roomForm.value).subscribe({
          next: (response: any) => {
            this.CloseModal();
            this.toastr.success(response.message);
          },
        });
      }
    } else {
      this.roomForm.markAllAsTouched();
    }
  }
}
