import { Component, OnInit } from '@angular/core';
import { ModalComponent } from '../../ui/modal/modal.component';
import { RoomFormComponent } from '../room-form/room-form.component';
import { ToastrService } from 'ngx-toastr';
import { RoomService } from '../../../services/room.service';
import { IRoom } from '../../shared/models/Room';
import { RoomType } from '../../shared/enums/RoomType';

@Component({
  selector: 'app-room',
  standalone: true,
  imports: [ModalComponent, RoomFormComponent],
  templateUrl: './room.component.html',
  styleUrl: './room.component.scss',
})
export class RoomComponent implements OnInit {
  isModalOpen = false;
  rooms: IRoom[] = [];
  room!: IRoom;

  constructor(
    private roomService: RoomService,
    private toastr: ToastrService,

  ) { }

  ngOnInit(): void {
    this.getAllRooms();
  }

  getAllRooms() {
    this.roomService.getAllRooms().subscribe({
      next: (response) => {
        if (response.data) {
          this.rooms = response.data;
        }
      }
    });
  }

  loadRoom(room: IRoom) {
    this.room = room;
    this.openModal();
  }

  deleteRoom(id: number) {
    this.roomService.deleteRoom(id).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.getAllRooms();
      },
    });
  }

  //iskoristi matDialog
  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.getAllRooms();
  }

  roomTypeToString(roomType: RoomType): string {
    switch (roomType) {
      case RoomType.Apartment:
        return 'Apartment';
      case RoomType.Sweet:
        return 'Sweet';
      case RoomType.Duplex:
        return 'Duplex';
      default:
        return '';
    }
  }
}
