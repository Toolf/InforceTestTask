import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ShortUrlService } from 'src/app/services/short-url.service';

@Component({
  selector: 'app-add-new-url',
  templateUrl: './add-new-url.component.html',
  styleUrls: ['./add-new-url.component.css']
})
export class AddNewUrlComponent {
  newUrl = ""

  urlAlreadyExists = false;

  constructor(private shortUrlService: ShortUrlService, private router: Router) { }

  onSubmit() {
    this.shortUrlService.create(this.newUrl).subscribe({
      next: (value) => {
        if (value == -1) {
          this.urlAlreadyExists = true;
        } else {
          this.router.navigate(['/short-urls']);
        }
      }
    })
  }
}
