#!/bin/sh

[ $# -lt 1 ] && echo unehre && exit 1

mkdir -p out

for u in $(./mediafromurl.sh "$1") ; do
        curl -o "out/$(echo "$u" | sed 's/^.*\/\(.*\)$/\1/')" "$u"
done
