update Photos
set IsMain = 0
where UserId = 1 and Id = 9;

update Photos
set IsMain = 1
where UserId = 1 and Id = 11;

select * from photos
where UserId = 1;
