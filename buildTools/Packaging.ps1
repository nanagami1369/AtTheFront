$workingDirectory = $Args[0]
# 圧縮
Copy-Item -Recurse $workingDirectory "$workingDirectory\..\AtTheFront"
7z a $workingDirectory\..\AtTheFront.zip "$workingDirectory\..\AtTheFront"
# ハッシュ生成
(Get-FileHash -Algorithm SHA512 -Path $workingDirectory\..\AtTheFront.zip).Hash > "$workingDirectory\..\AtTheFront_zip_sha512.hash"
Remove-Item -Recurse "$workingDirectory\..\AtTheFront"
