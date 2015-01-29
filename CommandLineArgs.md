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

Of course you can build some pipelines for generating AssetBundles.




###import
Importing resources from outside of Unity to inside.
Use for convert resources and import.
デフォルトの読み込み元フォルダは PROJECT_FOLDER/Resources(AssetRails_Importable) です
☆import処理を記述するランナーはこちら


	... -executeMethod AssetRailsController.Import + options

	import [-d] [-i] [-u] [-o path] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all resources has unique name.
	-o	--order-by-bundle-name	set import "for each bundleName folder". it takes more time.
	-s	--source-path	set source path of data. default is "Resources(AssetRails)".
	-e	--export-path	set additional output path of data.


☆内容はcleanするまでキャッシュされる


###prefabricate
bundleNameごとにprefabの作成などが行える。
prefabを作ったり、保存したりするのに使ってください。
☆prefabricate処理を記述するランナーはこちら

	... -executeMethod AssetRailsController.Prefabricate + options

	prefabricate [-d] [-i] [-u] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all resources has unique name.
	-s	--source-path	set source path of data. default is import cache.
	-e	--export-path	set additional output path of data.


☆内容はcleanするまでキャッシュされる


###bundlize
bundleNameごとにAssetBundleの作成を行うことができる。
AssetBundleを作成するのに使ってください。
複数プラットフォームのAssetBundleの同時作成が高速に行える。
サイズ、crcを自動的に記録する機能がある。
高速化モードあり。
☆bundlize処理を記述するランナーはこちら

	... -executeMethod AssetRailsController.Bundlize + options

	bundlize [-d] [-i] [-u] [-f] [-c] [-o path] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all AssetBundle resources has unique name.
	-f	--fast	execute fast convert, but delete import, prefabricate, versioning cache all.
	-c	--category-based-runner	use runner class which is Category_BundlizerBase based.
	-o	--output-memo-path	set output path of bundlize memo file. e.g. SOMEWHERE/FILENAME.
	-s	--source-path	set source path of data. default is prefabricate cache.
	-e	--export-path	set additional output path of data.


☆内容はcleanするまでキャッシュされる


###versioning
AssetBundleをプラットフォームごとにバージョン付けし、切り出すことができる。
デフォルトの吐き出し先は
過去のversionを指定してAssetBundleを読み出し、新規追加分と合わせて切り出すことも可能。

	... -executeMethod AssetRailsController.Versioning + options

	versioning [-d] [-i] [-v #] [-p str] [-c] [-f path] [-b #] [-x path] [-o path] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-v	--version	set versioning version by number.
	-p	--platform	set versioning platform by string. e.g. "WebPlayer", "iPhone".
	-c	--crc	use crc parameter. if crc is not contained in bundlize cache, crc will become "0".
	-f	--from-pool-path	set the source path of another "VersionedPool(AssetRails)".
	-b	--base-version	set base version which contained from-pool-path.
	-x	--exclude-assets	path of JSON file. contains the list of AssetBundle names which wants to be excluded from new vesion.
	-o	--output-list-path	set additional versioning list output path. e.g. SOMEWHERE/FILENAME.
	-s	--source-path	set source path of data. default is bundlize cache.
	-e	--export-path	set additional output path of data.

☆パラメータの組み合わせについて、別途死霊が必要。base-versionが複雑。付随してexclde
☆cleanに関係なく、対象のversion/platform ペアがすでに存在すれば、消去後に吐き出す。
☆デフォルトの吐き出し先についての記述、フォルダ絵





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


##common path rule:
The path representation which does not start with "/" is relative to PROJECT_FOLDER.
Also can use absolute path.

	e.g.
		a/b/c => PROJECT_FOLDER/a/b/c
