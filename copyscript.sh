#!/bin/bash

# --- Configuration ---
GITHUB_USER="your_username"
GITHUB_TOKEN="your_pat_token"
GITHUB_REPO="owner/repository"  # e.g., 'edwarddonner/llm-course'
FILE_TO_COPY="path/to/file.txt" # Path relative to repo root
NAS_DEST_PATH="/mnt/nas/destination_folder"

# --- Setup ---
TEMP_DIR=$(mktemp -d)
REPO_URL="https://${GITHUB_USER}:${GITHUB_TOKEN}@github.com/${GITHUB_REPO}.git"

# --- Execution ---
echo "Cloning repository to temporary directory..."
git clone --depth 1 "$REPO_URL" "$TEMP_DIR" # --depth 1 for a faster, shallow clone

if [ -f "$TEMP_DIR/$FILE_TO_COPY" ]; then
    echo "Copying $FILE_TO_COPY to $NAS_DEST_PATH..."
    cp "$TEMP_DIR/$FILE_TO_COPY" "$NAS_DEST_PATH"
    echo "Success!"
else
    echo "Error: File $FILE_TO_COPY not found in the repository."
fi

# --- Cleanup ---
rm -rf "$TEMP_DIR"
