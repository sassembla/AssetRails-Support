#AssetRails Overview and the caution

what is this:  
-> the tool for generating AssetBundle.


##Generate AssetBundle using command line
AssetRails can run with command line.
	APIドキュメントへのリンク付ける。

##"Routes" and "Runners"
☆素材のimport, prefab作成、bundle化、などを個別に実行可能
☆同じrouteでも設定が異なる処理を走らせられる。
* Run multiple routes in order.
Supported format is json & toml(experimental).
☆各routeでの素材のexportが可能。
Unity Editor上で途中経過を確認できる。
* AssetRails has Runner-API for each route.
You can programming it's runner.


##Web console & Jenkins job
☆Console でブラウザから実行状態が確認できる
☆画像
* you can run Jenkins job from AssetRails Console.


##Folder format for make AssetBundle
☆カテゴリー/バンドル名/素材 というフォーマットがある
☆.metaがついているリソースを持ち込むことが可能


##Manage AssetBundles
☆使用するAssetBundleのリストが作れる(Unity5でデフォルトで入ったけどな！)
☆プラットフォーム、バージョンごとにAssetBundleを保持
☆過去作ったAssetBundleを管理することが出来る
☆VersionedPool(AssetRails) 出力場所を表す記述


#Caution
☆AssetRails can only run under メタがでてくるモード。
☆AssetBundleを作るにはUnityのProライセンスとかが必要です。このAssetはそのへんを回避するものでは無いです。
☆Unity5での動作はbeta中は未保証だよっ


##Online Support
Let's issue,, but, if you won't,

fill the check list below

**checklist:**

1. Unity version : Unity ~
2. Platform: Win7,8.x, or Mac OS X 10.x.x

then

please contact
[https://twitter.com/sassembla](https://twitter.com/sassembla)
or
[sassembla@mac.com](mailto:sassembla@mac.com)
