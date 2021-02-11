import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() onCancelRegister = new EventEmitter<boolean>();
  model: any = {};

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe(
      (res) => {
        console.log(res);
        this.cancel();
      },
      (err) => console.log(err)
    );
  }

  cancel() {
    this.onCancelRegister.emit(false);
  }
}
