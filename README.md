# Curve Editor カーブコピー
AviUtlプラグインである [Curve Editor](https://github.com/mimaraka/aviutl-plugin-curve_editor) との連携外部ツールです。

AUPファイルに保存されているカーブ情報を、他のAUPファイルに移し替えることができます。

## ダウンロード・起動方法
https://github.com/Garech-mas/AviUtl_Curve_Editor_Copier/releases/latest

上記リンクにアクセスした後、以下の2つから選んでダウンロードしてください。
- `curve_editor_copier_v1.0.zip` を選択した場合、容量は大きくなるものの、すぐに起動することができます。
- ``curve_editor_copier_v1.0_requires_runtime.zip`` を選択した場合、追加で **.NET 6 ランタイム** をインストールする必要があります。
  - 未インストールの場合、初回起動時にエラーダイアログが出現します。画面の指示に従ってインストールしてください。
 
zipファイルを展開し、同梱されている `Curve Editor カーブコピー.exe` をダブルクリックすることで起動できます。

## 使用方法
![image](https://github.com/user-attachments/assets/b09b09c1-e38f-4792-bfb7-e72e39eed014)

1. `移動元`入力欄に、コピーしたいカーブ情報が入っているAUPファイルを指定します。
2. `移動先`入力欄に、コピーさせたいAUPファイルを指定します。
  - 右側の3点リーダ―部分からファイル選択ダイアログが出現するほか、エクスプローラからAUPファイルをドラッグ＆ドロップすることでも指定できます。
3. `実行`ボタンを押すことで、コピー処理が完了します。
  - この時、一緒に移動先ファイルに`.aup_backup`ファイルが生成されます。
    
    （万が一コピー処理によって移動先のファイルが壊れてしまった場合、`_backup`部分を外すことで元の状態に戻せます。）

## 注意点
- Curve Editor v2.0で作成したカーブをCurve Editor v1.Xで保存したAUPファイルにコピーした場合、正常に読み込めなくなる恐れがあります。

  移動元で使用していたCurve Editorのバージョンが導入されているAviUtlで読み込んでください。


## Special Thanks
AupDotNet - https://github.com/karoterra/AupDotNet
