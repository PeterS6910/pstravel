# Iniitlaize database

- drop database `afitraval`
- remove `Migrations` folder

Then execute following commands:
```
> dotnet ef migrations add InitialCreate
> dotnet ef database update
```

Then execute `Data/sk.sql` inside the database `afitraval`.