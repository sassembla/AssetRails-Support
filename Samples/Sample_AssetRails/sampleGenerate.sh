# remove version 1 for generating new version 1 AssetBundles everytime.
file="./VersionedPool(AssetRails)/1"
if [ -d "$file" ]; then
	rm -rf "$file"
fi

# generate new version 1.
sh Assets/AssetRails/SampleScripts/ShellScript/run.sh