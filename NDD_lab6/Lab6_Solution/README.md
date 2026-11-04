# Lab 6 - Phân trang, lọc + phân trang, tìm kiếm + phân trang

Làm tiếp từ `Lab5_Solution` (đã có ViewComponent `Major` và lọc bằng AJAX). Chỉ project **MyWebApp** thay đổi.

## Các file thêm / sửa so với Lab 5
| File | Nội dung |
|---|---|
| `Controllers/LearnerController.cs` | Biến `pageSize = 3`; `Index(int? mid)` trả trang đầu + `ViewBag.pageNum`; thêm action `LearnerFilter(int? mid, string? keyword, int? pageIndex)` |
| `Views/Learner/LearnerTable.cshtml` | Bảng Learner + thanh nav-phân trang (mỗi `page-item` mang `data-page`, `data-mid`, `data-keyword`) |
| `Views/Learner/Index.cshtml` | Form tìm kiếm (`#keyword`, `#btnSearch`) + AJAX cho click page-item / chuyên ngành / nút Search |
| `Views/Shared/MyLayout.cshtml` | Thêm Font Awesome (icon kính lúp) |

## Cách chạy
1. Mở `MyWebApp/appsettings.json`, thay `YOUR_PASSWORD` bằng mật khẩu `sa`.
2. Mở `MyWebApp.sln`, đặt `MyWebApp` làm Startup Project, chạy (F5) -> `/Learner`.
3. DbInitializer chỉ tạo sẵn 2 Learner, nên hãy bấm **Create New Student** thêm vài người (>= 4 người) để thấy thanh phân trang (mỗi trang 3 dòng; đổi ở `pageSize`).
4. Thử: bấm số trang; bấm chuyên ngành rồi bấm số trang; gõ từ khóa (ví dụ `n`) rồi bấm Search và bấm số trang.
