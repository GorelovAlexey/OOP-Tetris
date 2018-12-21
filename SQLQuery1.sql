SELECT * FRom People

Select * FRom Tag

select a.name FROM Tag a
inner join PeopleTag b on a.Id = b.TagId
where b.PeopleId = 2;