import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShortUrlInfo } from 'src/app/models/short-url-info';
import { ShortUrlService } from 'src/app/services/short-url.service';

@Component({
  selector: 'app-short-url-info',
  templateUrl: './short-url-info.component.html',
  styleUrls: ['./short-url-info.component.css']
})
export class ShortUrlInfoComponent {
  shortUrl: ShortUrlInfo | null = null;

  constructor(
    private route: ActivatedRoute,
    private shortUrlService: ShortUrlService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.fetchShortUrlDetails(parseInt(id));
    }
  }

  fetchShortUrlDetails(id: number) {
    this.shortUrlService.getShortUrlById(id).subscribe((shortUrl) => {
      this.shortUrl = shortUrl;
    });
  }
}
