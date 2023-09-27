export interface PaginationResponse<T> {
    page: number;
    perPage: number;
    total: number;
    count: number;
    data: Array<T>;
}
