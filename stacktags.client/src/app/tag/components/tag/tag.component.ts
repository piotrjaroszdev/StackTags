import { Component, OnInit } from "@angular/core";
import { Tag } from "../../models/Tag";
import { TagService } from "../../services/TagService";

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
})
export class TagComponent implements OnInit {
  tags: Tag[] = [];
  page = 1;
  pageSize = 20;
  sortBy = 'name';
  order = 'asc';

  constructor(private tagService: TagService) {}

  ngOnInit() {
    this.loadTags();
  }

  loadTags() {
    this.tagService.getTags(this.page, this.pageSize, this.sortBy, this.order)
      .subscribe((data: Tag[]) => this.tags = data);
  }

  changeSort(field: string) {
    if (this.sortBy === field) {
      this.order = this.order === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = field;
      this.order = 'asc';
    }
    this.loadTags();
  }

  nextPage() {
    this.page++;
    this.loadTags();
  }

  prevPage() {
    if (this.page > 1) this.page--;
    this.loadTags();
  }

  sync() {
    this.tagService.syncTags().subscribe(() => this.loadTags());
  }
}

