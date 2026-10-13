<!-- azure-cor-disclaimer -->
> **Important:** This skill provides guidance and recommended instructions to assist the AI system. Outputs are not guaranteed to be complete, correct, secure, or applicable to every scenario. Results should be reviewed and validated by a human before being applied. The AI model may choose not to follow all instructions exactly, and additional verification may be required.

# CI/CD Pipeline Patterns

CI/CD is deferred to v2. Do NOT auto-generate workflow files.

If the user requests CI/CD guidance, call `mcp_azure_mcp_deploy` → `deploy_pipeline_guidance_get` with `is-azd-project: false`, `pipeline-platform: 'github-actions'`, `deploy-option: 'provision-and-deploy'` and present the guidance.
