import { Component } from '@angular/core';
import { ShortUrlShortInfo } from 'src/app/models/short-url';
import { ShortUrlService } from 'src/app/services/short-url.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-short-urls-table',
  templateUrl: './short-urls-table.component.html',
  styleUrls: ['./short-urls-table.component.css']
})
export class ShortUrlsTableComponent {
  shortUrls: ShortUrlShortInfo[] = [];
  currentPage = 1;
  itemsPerPage = 10; // Adjust as needed
  totalItems = 0;
  displayedShortUrls: ShortUrlShortInfo[] = [];

  constructor(private tokenStorage: TokenStorageService, private shortUrlService: ShortUrlService) { }

  ngOnInit(): void {
    this.fetchShortUrls();
  }

  fetchShortUrls() {
    this.shortUrlService.getShortUrls(this.currentPage, this.itemsPerPage).subscribe((paginationResponse) => {
      this.shortUrls = paginationResponse.data;
      this.totalItems = paginationResponse.total;
    });
  }

  onPageChange(pageNumber: number) {
    this.currentPage = pageNumber;
    this.fetchShortUrls();
  }

  getPageArray(): number[] {
    const totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  isAuthorizedToDelete(shortUrl: ShortUrlShortInfo): boolean {
    let user = this.tokenStorage.getUser();
    return user?.userRole == "admin" || user?.userId == shortUrl.createdBy
  }

  isAuthorizedToAddNewUrl(): boolean {
    let user = this.tokenStorage.getUser();
    return user != null;
  }

  onDeleteShortUrl(shortUrlId: number) {
    this.shortUrlService.delete(shortUrlId).subscribe({
      next: (deleted) => {
        if (deleted) {
          this.fetchShortUrls();
        }
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

}
