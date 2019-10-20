#!/bin/sh
curl -fsSL "$1" | sed 's/["<>'"'"' ]/\n/g' | grep '^https://www\.gymnasium-pegnitz\.de' | grep -v '\.js' | grep -E '\.[wavjpeg]+$' | grep -v '\.pdf$' | sort | uniq
