# run routes written in json.
/Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd)\
	-executeMethod AssetRailsController.RunJson\
	-f $(pwd)/Assets/AssetRails/SampleScripts/plan.json


# example toml.
# /Applications/Unity/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath $(pwd)\
# 	-executeMethod AssetRailsController.RunToml\
# 	--file-path $(pwd)/Assets/AssetRails/SampleScripts/plan.toml

