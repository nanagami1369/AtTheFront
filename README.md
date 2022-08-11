# AtTheFront

![image](https://user-images.githubusercontent.com/38910015/184054098-5327b0e0-0226-4370-a5d2-1c31905216b4.png)

任意のグローバルキーボードショートカット押すことで、フォーカスが当たっているウィンドウを最前面に固定する(切り替える)

常駐型のアプリです。

デフォルトだと Win+Insert

## 実行方法

```cmd
REM Win+Insert
AtTheFront.exe
REM Alt+O
AtTheFront.exe "Alt+O"
REM Ctrl+Shift+K
AtTheFront.exe "Ctrl+Shift+K"
```

## build

```cmd
git clone https://github.com/nanagami1369/AtTheFront/
cd AtTheFront\
dotnet build

```

## スタートアップに登録する方法

1. `Win+R`を押す
2. 入力ボックスに`shell:startup`と入力
3. `OK`をクリック

![image](https://user-images.githubusercontent.com/38910015/183909967-a264753f-7a14-4c44-a5d2-3a7984bec45e.png)

4. 開いたExplorerの何もないところを右クリック→`新規作成`→`ショートカット`

![image](https://user-images.githubusercontent.com/38910015/183910457-646cdadf-bcac-4980-ae89-b0cb1f4d82fa.png)

5. 入力欄に`{{HogeHoge}}\AtTheFront.exe {{HotKey}}`と入力
6. 次へ

![image](https://user-images.githubusercontent.com/38910015/183911076-c75eda34-3dea-4a05-9999-080694d92e28.png)

7. 完了

![image](https://user-images.githubusercontent.com/38910015/183911552-df98633c-1c57-4f00-86ef-dde7096e281b.png)
