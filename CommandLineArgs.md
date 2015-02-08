#AssetRails command line args


Generate AssetBundles via command line:

Mac:

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd) -executeMethod AssetRailsController.Import

Win:

	"C:\Program Files (x86)\Unity\Editor\Unity.exe" -quit -projectPath "PROJECT_FOLDER_PATH" -executeMethod AssetRailsController.Import


##about Route:

AssetRails has the unit called "route".  
"Import resource" function is defined as "import" route,
"bundling some assets into one AssetBundle" function is also defined as "bundlize" route in AssetRails.  

Of course you can build some route-pipelines for generating AssetBundles.




##import
Importing resources from outside of Unity to inside.
Use for convert resources and import.

The default source folder of import route is  
"PROJECT_FOLDER/Resources(AssetRails)".


	... -executeMethod AssetRailsController.Import + options

	import [-d] [-i] [-u] [-o path] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all resources has unique name.
	-o	--order-by-bundle-name	set import "for each bundleName folder". it takes more time.
	-s	--source-path	set source path of data. default is "Resources(AssetRails)".
	-e	--export-path	set additional output path of data.

####runner for import
[importer](https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#importer)



##prefabricate
Generate prefabs for each bundleName.

	... -executeMethod AssetRailsController.Prefabricate + options

	prefabricate [-d] [-i] [-u] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all resources has unique name.
	-s	--source-path	set source path of data. default is import cache.
	-e	--export-path	set additional output path of data.

####runner for prefabricate
[prefabricator](https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#prefabricator)


##bundlize
Can Generate AssetBundles for each bundleName.

The sizes & crcs of AssetBundles are automatically recorded.

This "size" parameter is the size of AssetBundle file.
When you set 	BuildAssetBundleOptions.UncompressedAssetBundle option at generate AssetBundle, 

	... -executeMethod AssetRailsController.Bundlize + options

	bundlize [-d] [-i] [-u] [-f] [-c] [-o path] [-s path] [-e path]

	-d	--dryrun	run without do anything.
	-i	--info	show detail in log.
	-u	--unique	check if all AssetBundle resources has unique name.
	-a	--actual-size add the size parameter of the actual(uncompressed) AssetBundle's size to bundlizer result.
	-f	--fast	execute fast convert, but delete import, prefabricate, versioning cache all.
	-c	--category-based-runner	use runner class which is Category_BundlizerBase based.
	-o	--output-memo-path	set output path of bundlize memo file. e.g. SOMEWHERE/FILENAME.
	-s	--source-path	set source path of data. default is prefabricate cache.
	-e	--export-path	set additional output path of data.

####runner for bundlize
[bundlizer](https://github.com/sassembla/AssetRails-Support/blob/master/RunnersAPIDocument.md#bundlizer)


##versioning
Pool & Pick up generated AssetBundles as version.

The default output place is  
"PROJECT_FOLDER/VersionedPool(AssetRails)"

You can also pick up old-versioned AssetBundles as a part of new versioned AssetBundles.

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

####versioning in deep
[Versioning & driving bundled datas](https://github.com/sassembla/AssetRails-Support/blob/master/Versioning.md)





##clean
Delete all datas and caches under AssetRails contorol.
If you want to clean all datas inside AssetRails but want to use "already imported" data, should use -s --source-path & -e --export-path options of import | prefabricate | bundlize | versioning routes.

	... -executeMethod AssetRailsController.Clean


##setup
Switch editor-running platforms.

	... -executeMethod AssetRailsController.Setup + options

	setup [-w str]

	-w --work-platform	change editor platform to specified.
	

##teardown
Force quit AssetRails.

	... -executeMethod AssetRailsController.Teardown


#Running multiple routes in order

###runjson
Construct pipeline of routes by json.

	... -executeMethod AssetRailsController.RunJson + options

	runjson [-f path] [-c json]

	-f --file-path	run json file.
	-c --contents	directory run JSON format string.

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


##runtoml(experimental)
Construct pipeline of routes by toml.

	... -executeMethod AssetRailsController.RunToml + options

	runtoml [-f path]

	-f --file-path	run toml file.



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
		
		/a/b/c => /a/b/c
		
##Not only for AssetBundle, Use another format.
Specially "versioning" & "bundlize" has the functionality for generating & driving with AssetBundles(of Unity).

Although, you can generate "Not" AssetBundle format and pool these data files.