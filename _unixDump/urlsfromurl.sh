#!/bin/sh
curl -fsSL "$1" | sed 's/[<> ]/\n/g' | grep 'href=' | sed 's/^href=["'"'"']\(.*\)["'"'"']$/\1/g' | grep ^https://www.gymnasium-pegnitz.de/ | grep -v '\.css' | grep -v '\.wav' | grep -v '\.pdf' | grep -v '\.jpe\?g' | sort | uniq
