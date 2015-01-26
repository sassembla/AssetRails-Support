#AssetRails command line args


Generate AssetBundles via command line:

Mac:

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd) -executeMethod AssetRailsController.Import

Win:

	"C:\Program Files (x86)\Unity\Editor\Unity.exe"


##about Route:

AssetRails has "route" units.  
The way importing sequence is defined as "import" route,
The way bundling some assets into one AssetBundle sequence is also defined as "bundlize" route in AssetRails.  

Of course you can build some pipeline for generating your own AssetBundles.




###import
Importing resources from outside of Unity to inside.
Use for convert resources and import.
デフォルトの読み込み元フォルダは PROJECT_FOLDER/Resources(AssetRails_Importable) です
☆import処理を記述するランナーはこちら


	... -executeMethod AssetRailsController.Import + options

	import [-d] [-i] [-o path] [-s path] [-e path]

	-d --dryrun	ドライラン
	-i --info	詳細情報表示
	-o --order	bundleName単位でのimportを行う。 見やすいが遅い。
	-s --source-path	使用データパス指定
	-e --export-path	エクスポート先パス指定


###prefabricate
bundleNameごとにprefabの作成などが行える。
prefabを作ったり、保存したりするのに使ってください。
☆prefabricate処理を記述するランナーはこちら

	... -executeMethod AssetRailsController.Prefabricate + options

	prefabricate [-d] [-i] [-s path] [-e path]

	-d --dryrun	ドライラン
	-i --info	詳細情報表示
	-s --source-path	使用データパス指定
	-e --export-path	エクスポート先パス指定



###bundlize
bundleNameごとにAssetBundleの作成を行うことができる。
AssetBundleを作成するのに使ってください。
複数プラットフォームのAssetBundleの同時作成が高速に行える。
サイズ、crcを自動的に記録する機能がある。
高速化モードあり。
☆bundlize処理を記述するランナーはこちら

	... -executeMethod AssetRailsController.Bundlize + options

	bundlize [-d] [-i] [-f] [-s path] [-e path] [-o path]

	-d --dryrun	ドライラン
	-i --info	詳細情報表示
	-f --fast	高速化 ただしimport, prefabricate, versioningのキャッシュが消える。
	-c --category-based-runner runnerとして Category_BundlizerBase クラスのrunnerを使用する。
	-s --source-path	使用データパス指定
	-e --export-path	エクスポート先パス指定
	-o --output-memo-path	メモのアウトプット先パス指定


###versioning
AssetBundleをプラットフォームごとにバージョン付けし、切り出すことができる。
過去のversionを指定してAssetBundleを読み出し、新規追加分と合わせて切り出すことも可能。

	... -executeMethod AssetRailsController.Versioning + options

	versioning [-d] [-i] [-v #] [-p str] [-f path] [-b #] [-x jsonlist] [-c] [-o path] [-s path] [-e path]

	-d --dryrun	ドライラン
	-i --info	詳細情報表示
	-v --version	バージョンの指定(must)
	-p --platform プラットフォーム指定(must)
	-f --from-pool	デフォルト以外の読み出し元のVersionedPoolの指定
	-b --base-version	ベースにするバージョンの指定
	-x --exclude-assets	このversionに含まない、baseに含んでいるAssetBundleのnameリスト,JSON形式
	-c --crc	crcをversionedListに含む
	-o --output-list-path	versionedListのアウトプット先指定
	-s --source-path	使用データパス指定
	-e --export-path	エクスポート先パス指定

☆別途ドキュメントが必要な気がする

###clean
AssetRails内のtempフォルダを消す。AssetRails内にcacheされているリソースもすべて消える。

	... -executeMethod AssetRailsController.Clean


###setup
UnityEditorの動作するプラットフォームを切り替える

	... -executeMethod AssetRailsController.Setup + options

	setup [-w str]

	-w --work-platform	動作するプラットフォームを切り替える

###teardown
AssetRailsを強制終了させる。

	... -executeMethod AssetRailsController.Teardown


#Running multiple route in order

###runjson
jsonで複数のルートを通る

	... -executeMethod AssetRailsController.RunJson + options

	runjson [-f path] [-c json]

	-f --file-path	JSONで書かれたプランファイルを実行する
	-c --contents	JSONで書かれたプランを実行する

####the plan file written in Json:
run clean -> import -> prefabricate -> bundlize -> versioning iOS & Android AssetBundles.

plan.json

```
[
    {
        "clean": {}
    },
    {
        "import": {
            "--info": true
        }
    },
    {
        "prefabricate": {}
    },
    {
        "bundlize": {
            "--fast": true
        }
    },
    {
        "versioning": {
            "--version": 1,
            "--platform": "iPhone",
            "--crc": true
        }
    },
    {
        "versioning": {
            "--version": 1,
            "--platform": "Android",
            "--crc": true
        }
    }
]
```


###runtoml(experimental)
tomlでのプランニングと動作

	... -executeMethod AssetRailsController.RunToml + options

	runtoml [-f path]

	-f --file-path	Tomlで書かれたプランファイルを実行する



####the plan file written in Toml: (experimental, cannot write same route twice in one toml file.)

run clean -> import -> prefabricate -> bundlize -> versioning iOS & Android AssetBundles.

plan1.toml

```
title = "planning routes. but toml support is experimental."

[clean]

[import]
info = true

[prefabricate]

[bundlize]
fast = true

[versioning]
version = 1
platform = "iPhone"
crc = true
```

+

plan2.toml

```
title = "rest."

[versioning]
version = 1
platform = "Android"
crc = true
```

will be equal to json version.
