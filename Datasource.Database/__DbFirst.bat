# install tool: https://freesql.net/guide/db-first.html
# dotnet tool install -g FreeSql.Generator


FreeSql.Generator -Razor "__razor.cshtml.txt" -NameSpace "Database" -NameOptions 0,0,0,1 -DB "PostgreSQL,host=192.168.91.129;port=5433;username=dbuser_meta;password=DBUser.Meta;database=meta;pooling=true;maximum pool size=2" -Match "gov.*" -FileName "{name}.cs"
